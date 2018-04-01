using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {
    public static Vector3 MousePos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
	
}
