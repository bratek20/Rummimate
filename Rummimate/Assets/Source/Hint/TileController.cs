using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileController : MonoBehaviour {

    private TextMeshPro _text;
    private Vector3 _mouseOff;
    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshPro>();
    }

    public void Init(TileData data)
    {
        _text.color = data.GetColor();
        _text.text = data.Num.ToString();
    }

    public void SetPos(float x, float y)
    {
        transform.localPosition = new Vector3(x, y, -1);
    }

    private void OnMouseDown()
    {
        _mouseOff = transform.position - Utils.MousePos();
    }

    private void OnMouseDrag()
    {
        var pos = Utils.MousePos() + _mouseOff;
        SetPos(pos.x, pos.y);
    }
}
