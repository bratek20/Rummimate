using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class PlayerPanel : MonoBehaviour {
    public RectTransform Contest;
    public TMP_InputField ScoreLinePrefab;
    private List<TMP_InputField> _lines = new List<TMP_InputField>();
    private int _sum = 0;

    public void AddLine()
    { 
        var line = Instantiate(ScoreLinePrefab, Contest);
        _lines.Add(line);
    }

    public void AddEmptyLines(int num)
    {
        while(num-- > 0)
        {
            AddLine();
            LastLine().text = "-";
        }
    }

    public void RemoveLine()
    {
        if (_lines.Count > 0)
        {
            Destroy(_lines[_lines.Count - 1].gameObject);
            _lines.RemoveAt(_lines.Count - 1);
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
        int sign = str.Contains("-") ? -1 : 1;
        string toNum = Regex.Replace(str, "[^0-9.]", "");

        return sign * int.Parse(toNum);
    }

    public void SetLastScore(int roundScore, bool winner)
    {
        _sum += roundScore;
        var lastLine = LastLine();
        lastLine.text = roundScore + " ( " + _sum + " )";
    }

    public int GetSum()
    {
        return _sum;
    }
}
