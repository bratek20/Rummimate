using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileList : MonoBehaviour {
    public const int TILE_MAX_NUM = 13;
    public GameObject TileSpawnerPrefab;
    public RectTransform Content;

	// Use this for initialization
	void Start () {
        var tileRect = TileSpawnerPrefab.GetComponent<RectTransform>();
        tileRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Content.rect.width);
        float tileHeight = tileRect.rect.height;

        Content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tileHeight * TILE_MAX_NUM);
		for(int i=1; i<=TILE_MAX_NUM; i++)
        {
            var spawner = Instantiate(TileSpawnerPrefab, Content).GetComponent<TileSpawner>();
            spawner.Init(new TileData(i, TileData.Colors.Red));
        }
	}

    public Rect CalculateWorldRect()
    {
        Vector3[] corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);

        Vector3 topLeft = Utils.ToWorldPoint(corners[0]);
        Vector3 bottomRight = Utils.ToWorldPoint(corners[2]);
       
        return new Rect(topLeft, bottomRight - topLeft);
    }

}
