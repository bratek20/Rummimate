using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileData
{
    public enum Colors
    {
        Yellow,
        Red,
        Blue,
        Black
    }

    public int Num;
    public Colors Col;

    public TileData(int number, Colors color)
    {
        Num = number;
        Col = color;
    }

    public Color GetColor()
    {
        return ColorFromId(Col);
    }

    public static Color ColorFromId(Colors color)
    {
        switch ( color )
        {
            case Colors.Yellow:
                return Color.yellow;
            case Colors.Red:
                return Color.red;
            case Colors.Blue:
                return Color.blue;
            case Colors.Black:
                return Color.black;
            default:
                return Color.white;
        }
    }
}
