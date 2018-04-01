using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHolder : MonoBehaviour {

    private SpriteRenderer _sr;
	private void Awake () {
        _sr = GetComponent<SpriteRenderer>();
        float width = _sr.sprite.bounds.size.x;
        float height = _sr.sprite.bounds.size.y;
        Debug.Log("w: " + width + ", h: " + height);

        Rect camR = Utils.CameraRect();
        Debug.Log("w: " + camR.width + ", h: " + camR.height);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
