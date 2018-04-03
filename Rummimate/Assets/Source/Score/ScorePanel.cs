using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour {
    private static ScorePanel _instance;
    public static ScorePanel Get()
    {
        return _instance;
    }

    public RectTransform ScrollContent;
    public RectTransform InnerPanel;
    public RectTransform PlayerPanelPrefab;
    public RectTransform Buttons;

    public Button AddPlayerButton;
    public Button RemovePlayerButton;
    public Button AddLineButton;
    public Button RemoveLineButton;
    public Button ClearButton;

    public ConfirmDialog ConfirmDialog;

    private List<PlayerPanel> _playerPanels = new List<PlayerPanel>();
    private int _lines = 0;

    private void Awake()
    {
        _instance = this;

        AddListeners();

        var corutine = LateInit(0.01f);
        StartCoroutine(corutine);
    }

    private IEnumerator LateInit(float waitSec)
    {
        yield return new WaitForSeconds(waitSec);

        LoadScores();

        RecalculateSize();
    }

    private void OnDestroy()
    {
        SaveScores();

        RemoveListeners();
    }

    private void AddListeners()
    {
        AddPlayerButton.onClick.AddListener(AddPlayerPanel);
        RemovePlayerButton.onClick.AddListener(RemovePlayerHandler);
        ClearButton.onClick.AddListener(ClearHandler);
        AddLineButton.onClick.AddListener(AddLine);
        RemoveLineButton.onClick.AddListener(RemoveLineHandler);
    }

    private void RemoveListeners()
    {
        AddPlayerButton.onClick.RemoveListener(AddPlayerPanel);
        RemovePlayerButton.onClick.RemoveListener(RemovePlayerHandler);
        ClearButton.onClick.RemoveListener(ClearHandler);
        AddLineButton.onClick.RemoveListener(AddLine);
        RemoveLineButton.onClick.RemoveListener(RemoveLineHandler);
    }

    private void AddPlayerPanel()
    {
        var panel = Instantiate(PlayerPanelPrefab, InnerPanel).GetComponent<PlayerPanel>();
        _playerPanels.Add(panel);
        panel.AddEmptyLines(_lines);

        RecalculateSize();
    }

    private void RemovePlayerPanel(string name)
    {
        var panel = _playerPanels.Find(p =>
        {
            return p.Name.text == name;
        });

        if ( panel != null )
        {
            _playerPanels.Remove(panel);
            Destroy(panel.gameObject);
            RecalculateSize();
        }
    }

    private void AddLine()
    {
        if(_playerPanels.Count == 0)
        {
            return;
        }

        int[] scores = new int[_playerPanels.Count];
        int minScore = int.MaxValue;
        int winnerI = -1;
        int sum = 0;

        for ( int i = 0; i < scores.Length; i++ )
        {
            scores[i] = _playerPanels[i].GetLastScore();
            sum += scores[i];
            if ( minScore > scores[i] )
            {
                minScore = scores[i];
                winnerI = i;
            }
        }

        for ( int i = 0; i < scores.Length; i++ )
        {
            if ( i == winnerI )
            {
                continue;
            }
            _playerPanels[i].AddLine(-scores[i]);
        }

        int reward = sum - scores[winnerI]; // sum of losers
        int penalty = scores[winnerI]; // my penalty (almost always is zero)
        _playerPanels[winnerI].AddLine(reward - penalty);
        _lines++;
    }

    private void RemoveLine()
    {
        if(_lines == 0)
        {
            return;
        }

        _playerPanels.ForEach(panel =>
        {
            panel.RemoveLine();
        });
        _lines--;
    }

    private void Clear()
    {
        _playerPanels.ForEach(panel =>
        {
            Destroy(panel.gameObject);
        });

        _lines = 0;
        _playerPanels.Clear();
        RecalculateSize();
    }

    private void RecalculateSize()
    {
        float panelWidth = PlayerPanelPrefab.rect.width;
        float width = _playerPanels.Count * panelWidth;
        InnerPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

        float scrollWidth = width + 2 * Buttons.rect.width;
        ScrollContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollWidth);      

        var pos = Utils.GlobalCorner(InnerPanel, 2);
        Buttons.transform.position = new Vector3(pos.x, pos.y, 0);
    }

    private const string DATA_KEY = "LastScores";
    private void LoadScores()
    {
        Clear();

        string dataAsJson = PlayerPrefs.GetString(DATA_KEY, "");
        if(dataAsJson=="")
        {
            return;
        }

        ScoreData scoreData = JsonUtility.FromJson<ScoreData>(dataAsJson);
        scoreData.PlayerData.ForEach(pData =>
        {
            AddPlayerPanel();
            _playerPanels[_playerPanels.Count - 1].LoadData(pData);
        });
        _lines = scoreData.Lines;
    }

    private void SaveScores()
    {
        ScoreData scoreData = new ScoreData();
        scoreData.Lines = _lines;
        _playerPanels.ForEach(player =>
        {
            scoreData.PlayerData.Add(player.GetData());
        });

        string dataAsJson = JsonUtility.ToJson(scoreData);
        PlayerPrefs.SetString(DATA_KEY, dataAsJson);
    }

    private void ClearHandler()
    {
        ConfirmData data = new ConfirmData();
        data.Message = "Napisz \"Confirm\" i kliknij przycisk Confirm by usunac wszystkich graczy";
        data.OnConfirm = ClearByConfirm;
        ConfirmDialog.Open(data);
    }

    private void ClearByConfirm(string answer)
    {
        string lowerAns = answer.ToLower();
        if( lowerAns == "confirm" || lowerAns == "\"confirm\"" )
        {
            Clear();
        }
    }

    private void RemovePlayerHandler()
    {
        ConfirmData data = new ConfirmData();
        data.Message = "Wpisz imie gracza i nacisnij przycisk Confirm by go usunac";
        data.OnConfirm = RemovePlayerPanel;
        ConfirmDialog.Open(data);
    }

    private void RemoveLineHandler()
    {
        ConfirmData data = new ConfirmData();
        data.Message = "Napisz \"Confirm\" i kliknij przycisk Confirm by usunac ostatnia linie";
        data.OnConfirm = RemoveLineByConfirm;
        ConfirmDialog.Open(data);
    }

    private void RemoveLineByConfirm(string answer)
    {
        if ( answer == "Confirm" || answer == "\"Confirm\"" )
        {
            RemoveLine();
        }
    }
}


