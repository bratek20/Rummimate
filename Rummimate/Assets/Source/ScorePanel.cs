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

    private Vector3 _startInnerPos;
    private List<PlayerPanel> _playerPanels = new List<PlayerPanel>();

    private void Awake()
    {
        _instance = this;

        AddListeners();

        _startInnerPos = InnerPanel.transform.localPosition;
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

        RecalculateSize();
    }

    private void AddLine()
    {
        _playerPanels.ForEach(panel =>
        {
            panel.AddLine();
        });

    }

    private void RemoveLine()
    {
        _playerPanels.ForEach(panel =>
        {
            panel.RemoveLine();
        });
    }

    private void Clear()
    {
        _playerPanels.ForEach(panel =>
        {
            Destroy(panel.gameObject);
        });

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
