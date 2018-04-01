using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {
    public static Vector3 MousePos()
    {
        return ToWorldPoint(Input.mousePosition);
    }
	
    public static Vector3 ToWorldPoint(Vector3 screenPoint)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(screenPoint);
        pos.z = 0;
        return pos;
    }

    public static Rect ToWorldRect(Rect screenRect)
    {
        Vector3 topLeft = ToWorldPoint(new Vector3(screenRect.xMin, screenRect.yMin));
        Vector3 bottomRight = ToWorldPoint(new Vector3(screenRect.width, screenRect.height));
        return new Rect(topLeft, bottomRight - topLeft);
    }

    public static Rect CameraRect()
    {
        var pixelRect = Camera.main.pixelRect;
        return ToWorldRect(pixelRect);
    }

    public static Vector3 GlobalCorner(RectTransform trans, int i)
    {
        Vector3[] coords = new Vector3[4];
        trans.GetWorldCorners(coords);

        return coords[i];
    }
}
