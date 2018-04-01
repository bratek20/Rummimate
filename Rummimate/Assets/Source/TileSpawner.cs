using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSpawner : MonoBehaviour {

    private void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(Spawn);
    }


    private void Spawn()
    {
        Board.Get().AddTile(new TileData(6, TileData.Colors.Red));
    }
}
