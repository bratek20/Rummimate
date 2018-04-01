using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPanel : MonoBehaviour {
    public RectTransform Contest;
    public GameObject ScoreLinePrefab;
    public List<GameObject> _lines = new List<GameObject>();

    public void AddLine()
    {
        Debug.Log("add");
        var line = Instantiate(ScoreLinePrefab, Contest);
        _lines.Add(line);
    }

    public void RemoveLine()
    {
        Debug.Log("rem");
        if (_lines.Count > 0)
        {
            Destroy(_lines[_lines.Count - 1]);
            _lines.RemoveAt(_lines.Count - 1);
        }
    }
}
