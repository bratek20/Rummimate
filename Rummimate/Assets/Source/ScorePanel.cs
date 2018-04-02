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

    public RectTransform InnerPanel;
    public RectTransform PlayerPanelPrefab;
    public RectTransform Buttons;

    public Button AddPlayerButton;
    public Button AddLineButton;
    public Button RemoveLineButton;
    public Button ClearButton;

    private List<PlayerPanel> _playerPanels = new List<PlayerPanel>();
    private int _lines = 0;

    private void Awake()
    {
        _instance = this;

        AddListeners();

        RecalculateSize();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        AddPlayerButton.onClick.AddListener(AddPlayerPanel);
        ClearButton.onClick.AddListener(Clear);
        AddLineButton.onClick.AddListener(AddLine);
        RemoveLineButton.onClick.AddListener(RemoveLine);
    }

    private void RemoveListeners()
    {
        AddPlayerButton.onClick.RemoveListener(AddPlayerPanel);
        ClearButton.onClick.RemoveListener(Clear);
        AddLineButton.onClick.RemoveListener(AddLine);
        RemoveLineButton.onClick.RemoveListener(RemoveLine);
    }

    private void AddPlayerPanel()
    {
        float width = PlayerPanelPrefab.rect.width;
        var panel = Instantiate(PlayerPanelPrefab, InnerPanel).GetComponent<PlayerPanel>();
        _playerPanels.Add(panel);
        panel.AddEmptyLines(_lines);

        RecalculateSize();
    }

    private void AddLine()
    {
        if(_playerPanels.Count == 0)
        {
            return;
        }

        if(_lines > 0)
        {
            UpdateScore();
        }
        
        _playerPanels.ForEach(panel =>
        {
            panel.AddLine();
        });
        _lines++;

    }

    private void UpdateScore()
    {
        int[] scores = new int[_playerPanels.Count];
        int maxScore = int.MinValue;
        int winnerI = -1;
        int sum = 0;

        for (int i = 0; i < scores.Length; i++ )
        {
            scores[i] = _playerPanels[i].GetLastScore();
            sum += scores[i];
            if (maxScore < scores[i])
            {
                maxScore = scores[i];
                winnerI = i;
            }
        }

        for(int i = 0; i < scores.Length; i++ )
        {
            if(i == winnerI)
            {
                continue;
            }
            _playerPanels[i].SetLastScore(scores[i], false);
        }

        _playerPanels[winnerI].SetLastScore(-sum + scores[winnerI], true);
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

        var pos = Utils.GlobalCorner(InnerPanel, 2);
        Buttons.transform.position = new Vector3(pos.x, pos.y, 0);
    }
}
