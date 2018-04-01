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
}
