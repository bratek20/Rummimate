
using UnityEngine;

public enum Colors
{
    Yellow,
    Red,
    Blue,
    Black
}

public class ColorsHelper
{
    public static Colors Next(Colors current)
    {
        int next = ((int)current + 1) % 4;
        return (Colors)next;
    }

    public static Color Convert(Colors color)
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

