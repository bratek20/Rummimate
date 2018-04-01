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

    public GameObject TilePrefab;
    public void AddTile(TileData data)
    {
        var tile = Instantiate(TilePrefab, transform).GetComponent<TileController>();
        tile.SetPos(0, 0);
        tile.Init(data);
    }

    public void RemoveTile(TileController tile)
    {
        Debug.Log("Removing");

        //Destroy(tile.gameObject);
    }
}
