using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class PlayerPanel : MonoBehaviour {
    public RectTransform Content;
    public TMP_InputField ScoreLinePrefab;
    public TMP_InputField Name;

    private List<TMP_InputField> _lines = new List<TMP_InputField>();
    private List<int> _sums = new List<int>();
    private float _contentHeight;

    private void Awake()
    {
        _contentHeight = Content.rect.height;    
    }

    private TMP_InputField CreateLine()
    {
        var line = Instantiate(ScoreLinePrefab, Content);
        _lines.Add(line);

        return line;
    }

    public void AddLine(int roundScore)
    {
        var lastLine = LastLine();
        if(lastLine)
        {
            lastLine.enabled = false;
            SetLastScore(roundScore);
        }

        CreateLine();
        UpdateContentSize();
    }

    private void UpdateContentSize()
    {
        float curContentHeight = ScoreLinePrefab.GetComponent<RectTransform>().rect.height * (_lines.Count + 1);
        float contentY = Mathf.Max(curContentHeight - _contentHeight, 0f);

        Content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, curContentHeight);
        Content.transform.localPosition = new Vector3(0f, contentY);
    }

    public void AddEmptyLines(int num)
    {
        while(num-- > 0)
        {
            AddLine(0);
        }
    }

    public void RemoveLine()
    {
        if ( _lines.Count == 0 )
        {
            return;
        }

        Destroy(_lines[_lines.Count - 1].gameObject);
        _lines.RemoveAt(_lines.Count - 1);

        var lastLine = LastLine();
        if(lastLine != null)
        {
            lastLine.enabled = true;
            lastLine.text = LastRoundScore().ToString();
            _sums.RemoveAt(_sums.Count - 1);
        }
    }

    private TMP_InputField LastLine()
    {
        if(_lines.Count==0)
        {
            return null;
        }
        return _lines[_lines.Count - 1];
    }

    public int GetLastScore()
    {
        var lastLine = LastLine();
        if(lastLine == null)
        {
            return 0;
        }

        string str = lastLine.text;
        int res;
        int.TryParse(str, out res);
        return res;
    }

    private void SetLastScore(int roundScore)
    {
        _sums.Add(GetSum() + roundScore);
        var lastLine = LastLine();
        lastLine.text = GetSum() + " ( " + roundScore + " )";
    }

    private int LastRoundScore()
    {
        int size = _sums.Count;
        if(size == 0)
        {
            return 0;
        }
        if(size == 1 )
        {
            return _sums[0];
        }
        return _sums[size-1] - _sums[size-2];
    }

    private void Clear()
    {
        _sums.Clear();
        _lines.ForEach(line =>
        {
            Destroy(line.gameObject);
        });
        _lines.Clear();
    }

    public int GetSum()
    {
        int size = _sums.Count;
        if(size==0)
        {
            return 0;
        }
        return _sums[size-1];
    }

    public PlayerData GetData()
    {
        PlayerData playerData = new PlayerData();
        playerData.Name = Name.text;
        playerData.Sums = _sums;
        _lines.ForEach(line =>
        {
            playerData.Lines.Add(line.text);
        });

        return playerData;
    }

    public void LoadData(PlayerData playerData)
    {
        Clear();

        Name.text = playerData.Name;
        _sums = playerData.Sums;
        playerData.Lines.ForEach(lineStr =>
        {
            var line = CreateLine();
            line.text = lineStr;
        });

        UpdateContentSize();
    }
}
