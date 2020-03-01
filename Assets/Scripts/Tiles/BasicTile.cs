using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BasicTile : BaseTile
{
    public SpriteNeighbourMask[] m_Sprites;

    protected override Sprite GetSprite(SpriteNeighbour mask)
    {
        foreach (SpriteNeighbourMask sm in m_Sprites)
        {
            if (MatchesMask(sm, mask))
            {
                return sm.sprite;
            }
        }
        return m_Error;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a BasicTile Asset
    [MenuItem("Assets/Create/BasicTile")]
    public static void CreateTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Basic Tile", "New Basic Tile", "Asset", "Save Basic Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BasicTile>(), path);
    }
#endif
}
