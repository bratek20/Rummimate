using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    private static Board _instance;
    public static Board Get()
    {
        return _instance;
    }
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        TileList.Init();
        TileHolder.Init();

        Rect camR = Utils.CameraRect();
        Rect listR = TileList.CalculateWorldRect();
        BoxCollider2D colldier = GetComponent<BoxCollider2D>();

        float width = TileHolder.Width;
        float height = camR.height - TileHolder.Height;
        
        transform.position = new Vector3(listR.xMax + width/2, listR.yMax - height/2);
        colldier.size = new Vector2(width, height);
    }

    public TileList TileList;
    public TileHolder TileHolder;
    public GameObject TilePrefab;
    public void AddTile(TileData data)
    {
        var tile = Instantiate(TilePrefab, transform).GetComponent<TileController>();
        tile.SetPos(0, 0);
        tile.Init(data);
    }

    public void RemoveTile(TileController tile)
    {
        Destroy(tile.gameObject);
    }
}
