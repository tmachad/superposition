using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public abstract class BaseTile : Tile
{
    public Sprite m_Preview;
    public Sprite m_Error;

    protected readonly Vector3Int[] m_PositionsToCheck = {
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
            for (int xDelta = -1; xDelta <= 1; xDelta++)
            {
                Vector3Int otherPos = new Vector3Int(position.x + xDelta, position.y + yDelta, position.z);
                if (HasTile(tilemap, otherPos))
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
            mask += (byte)(HasTile(tilemap, position + m_PositionsToCheck[i]) ? Mathf.Pow(2, i) : 0);
        }

        tileData.sprite = GetSprite(mask);
    }

    /// <summary>
    /// Checks if the given position on the given tilemap contains a tile of the same type as this one.
    /// </summary>
    /// <param name="tilemap">The tilemap to look for a tile in.</param>
    /// <param name="position">The position to check for a tile.</param>
    /// <returns></returns>
    protected bool HasTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }

    /// <summary>
    /// Gets the sprite matching the given mask of tile positions.
    /// </summary>
    /// <param name="mask">A mask of neighbouring tile positions.</param>
    /// <returns></returns>
    protected abstract Sprite GetSprite(SpriteNeighbour mask);

    /// <summary>
    /// Checks if the given sprite is valid for the given mask of positions.
    /// </summary>
    /// <param name="spriteNeighbourMask">The sprite to check against the mask of positions.</param>
    /// <param name="mask">The mask of tile positions to compare against.</param>
    /// <returns></returns>
    protected bool MatchesMask(SpriteNeighbourMask spriteNeighbourMask, SpriteNeighbour mask)
    {
        // Convert optional positions to 1 in both masks so they don't effect the comparison
        SpriteNeighbour tempMask = mask | spriteNeighbourMask.optionalNeighboursMask;
        SpriteNeighbour tempReq = spriteNeighbourMask.requiredNeighboursMask | spriteNeighbourMask.optionalNeighboursMask;

        return tempMask == tempReq && spriteNeighbourMask.sprite;
    }
}
