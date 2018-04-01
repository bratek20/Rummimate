using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileSpawner : MonoBehaviour {
    private Button _button;
    public TextMeshProUGUI Text;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Spawn);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Spawn);
    }

    private TileData _data;
    public void Init(TileData data)
    {
        _data = data;

        Text.text = data.Num.ToString();
        Text.color = data.GetColor();
    }

    private void Spawn()
    {
        Board.Get().AddTile(_data);
    }
}
