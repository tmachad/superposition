using UnityEngine;

public enum SpriteNeighbour : int
{
    Top =           0b0000_0001,
    TopRight =      0b0000_0010,
    Right =         0b0000_0100,
    BottomRight =   0b0000_1000,
    Bottom =        0b0001_0000,
    BottomLeft =    0b0010_0000,
    Left =          0b0100_0000,
    TopLeft =       0b1000_0000
}

[System.Serializable]
public class SpriteNeighbourMask
{
    public Sprite sprite;
    public int requiredNeighboursMask;
    public int optionalNeighboursMask;
}
