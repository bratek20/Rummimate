using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileData
{
    public int Num;
    public Colors Col;

    public TileData(int number, Colors color)
    {
        Num = number;
        Col = color;
    }

    public Color GetColor()
    {
        return ColorsHelper.Convert(Col);
    }
}
