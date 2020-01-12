using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BasicTile : Tile
{
    public Sprite m_Preview;
    public Sprite m_Error;

    public SpriteNeighbourMask test;

    public SpriteNeighbourMask[] m_Sprites;

    private readonly Vector3Int[] m_PositionsToCheck = {
        new Vector3Int(-1, 1, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 1, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, -1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(1, -1, 0)
    };

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
        SpriteNeighbour mask = 0;
        for (int i = 0; i < m_PositionsToCheck.Length; i++)
        {
            mask += (byte)(HasBasicTile(tilemap, position + m_PositionsToCheck[i]) ? Mathf.Pow(2, i) : 0);
        }

        tileData.sprite = GetSprite(mask);
    }

    private bool HasBasicTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }

    private Sprite GetSprite(SpriteNeighbour mask)
    {
        Debug.Log($"Looking for sprite mask {Convert.ToString((byte)mask, 2)}");
        foreach (SpriteNeighbourMask sm in m_Sprites)
        {
            // Convert optional positions to 1 in both masks so they don't effect the comparison
            SpriteNeighbour tempMask = mask | sm.optionalNeighboursMask;
            SpriteNeighbour tempReq = sm.requiredNeighboursMask | sm.optionalNeighboursMask;

            if (tempMask == tempReq)
            {
                Debug.Log($"Found match: '{sm.sprite.name}' Req: {Convert.ToString((byte)sm.requiredNeighboursMask, 2)} Opt: {Convert.ToString((byte)sm.optionalNeighboursMask, 2)}");
                return sm.sprite;
            }
        }
        Debug.Log("No match found");
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
