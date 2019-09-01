using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BasicTile : Tile
{
    [System.Serializable]
    public class SpriteMasks
    {
        public Sprite sprite;
        public int requiredMask;
        public int optionalMask;
    }

    public Sprite m_Preview;
    public Sprite m_Error;

    public SpriteMasks[] m_Sprites;

    public Vector3Int[] m_PositionsToCheck;

    public SpriteNeighbourMask mask;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for (int yDelta = -1; yDelta <= 1; yDelta++)
        {
            for (int xDelta = -1; xDelta <=1; xDelta++)
            {
                Vector3Int otherPos = new Vector3Int(position.x + xDelta, position.y + yDelta, position.z);
                if (HasBasicTile(tilemap, otherPos))
                {
                    tilemap.RefreshTile(otherPos);
                }
            }
        }
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        int mask = 0;
        for (int i = 0; i < m_PositionsToCheck.Length; i++)
        {
            mask += HasBasicTile(tilemap, position + m_PositionsToCheck[i]) ? (int)Mathf.Pow(2, i) : 0;
        }

        tileData.sprite = GetSprite(mask);
    }

    private bool HasBasicTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }

    private Sprite GetSprite(int mask)
    {
        foreach (SpriteMasks sm in m_Sprites)
        {
            // Convert optional positions to 1 in both masks so they don't effect the comparison
            int tempMask = mask | sm.optionalMask;
            int tempReq = sm.requiredMask | sm.optionalMask;

            if (tempMask == tempReq)
            {
                return sm.sprite;
            }
        }

        return m_Error;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/BasicTile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Basic Tile", "New Basic Tile", "Asset", "Save Basic Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BasicTile>(), path);
    }
#endif
}
