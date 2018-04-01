using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHolder : MonoBehaviour {

    private SpriteRenderer _sr;

    public float HeightScale = 0.66f;
    public float Width { private set; get; }
    public float Height { private set; get; }

    private void Start () {
        _sr = GetComponent<SpriteRenderer>();
        float width = _sr.sprite.bounds.size.x;
        float height = _sr.sprite.bounds.size.y;

        Rect camR = Utils.CameraRect();
        Rect listR = Board.Get().TileList.CalculateWorldRect();

        Width = camR.width - listR.width;
        float scaleX = Width / width;
        float scaleY = scaleX * HeightScale;
        Height = height * scaleY;

        transform.localScale = new Vector3(scaleX, scaleY);
        transform.localPosition = new Vector3(listR.xMax + Width/2, listR.yMin + Height/2);
    }
	
}
