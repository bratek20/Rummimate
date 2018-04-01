using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileList : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnMouseDown()
    {
        Debug.Log("List MouseDown");
    }

    private void OnMouseDrag()
    {
        gameObject.transform.position = Utils.MousePos();
    }

    private void OnMouseUp()
    {
        Debug.Log("List MouseUp");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision enter");
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("collision exit");
    }
}
