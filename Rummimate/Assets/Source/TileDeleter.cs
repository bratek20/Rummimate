using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDeleter : MonoBehaviour {
    [SerializeField]
    private TileList _tileList; 
	// Use this for initialization
	void Start () {
        BoxCollider2D colldier = GetComponent<BoxCollider2D>();
        Rect worldRect = _tileList.CalculateWorldRect();

        transform.position = new Vector3(worldRect.center.x, worldRect.center.y);
        colldier.size = new Vector2(worldRect.width, worldRect.height);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var tile = collision.GetComponent<TileController>();
        if(tile != null)
        {
            Board.Get().RemoveTile(tile);
        }
    }
}
