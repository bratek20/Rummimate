using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileController : MonoBehaviour {
    private TextMeshPro _text;
    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>();
    }
    // Use this for initialization
    public void Init(TileData data)
    {
        _text.color = data.GetColor();
        _text.text = data.Num.ToString();
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void SetPos(float x, float y)
    {
        transform.localPosition = new Vector3(x, y, -1);
    }

    private void OnMouseDown()
    {
        //Debug.Log("MouseDown");
    }

    private void OnMouseDrag()
    {
        var pos = Utils.MousePos();
        SetPos(pos.x, pos.y);
    }

    private void OnMouseUp()
    {
        //Debug.Log("MouseUp");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision enter");
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("collision exit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger enter");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("trigger exit");
    }
}
