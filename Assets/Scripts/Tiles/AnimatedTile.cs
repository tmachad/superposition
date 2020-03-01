using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AnimatedTile : BaseTile
{
    public float m_AnimationSpeed = 1.0f;
    public AnimatedSpriteNeighbourMask[] m_Sprites;

    private AnimatedSpriteNeighbourMask m_CurrentSprite;

    public override bool GetTileAnimationData(Vector3Int location, ITilemap tileMap, ref TileAnimationData tileAnimationData)
    {
        if (m_CurrentSprite != null && m_CurrentSprite.animationSprites.Length > 0)
        {
            tileAnimationData.animatedSprites = m_CurrentSprite.animationSprites;
            tileAnimationData.animationSpeed = m_AnimationSpeed;
            tileAnimationData.animationStartTime = 0;
            return true;
        }
        return false;
    }

    protected override Sprite GetSprite(SpriteNeighbour mask)
    {
        foreach (AnimatedSpriteNeighbourMask sm in m_Sprites)
        {
            if (MatchesMask(sm, mask))
            {
                m_CurrentSprite = sm;
                return sm.sprite;
            }
        }
        return m_Error;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a AnimatedTile Asset
    [MenuItem("Assets/Create/AnimatedTile")]
    public static void CreateTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Animated Tile", "New Animated Tile", "Asset", "Save Animated Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<AnimatedTile>(), path);
    }
#endif
}
