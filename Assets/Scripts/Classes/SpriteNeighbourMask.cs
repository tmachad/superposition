using UnityEngine;

public enum SpriteNeighbour : int
{
    TopLeft =       0b0000_0001,
    Top =           0b0000_0010,
    TopRight =      0b0000_0100,
    Left =          0b0000_1000,
    Right =         0b0001_0000,
    BottomLeft =    0b0010_0000,
    Bottom =        0b0100_0000,
    BottomRight =   0b1000_0000,
}

[System.Serializable]
public struct SpriteNeighbourMask
{
    public Sprite sprite;
    public int requiredNeighboursMask;
    public int optionalNeighboursMask;
}
