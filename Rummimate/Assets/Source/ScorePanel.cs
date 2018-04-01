using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour {
    public RectTransform InnerPanel;
    public RectTransform PlayerPanelPrefab;
    public Button AddPlayerButton;
    public Button ClearButton;

    private Vector3 _startInnerPos;
    private List<RectTransform> _playerPanels = new List<RectTransform>();

    private void Awake()
    {
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
    }

    private void RemoveListeners()
    {
        AddPlayerButton.onClick.RemoveListener(AddPlayerPanel);
        ClearButton.onClick.RemoveListener(Clear);
    }

    private void AddPlayerPanel()
    {
        float width = PlayerPanelPrefab.rect.width;
        var panel = Instantiate(PlayerPanelPrefab, InnerPanel);
        _playerPanels.Add(panel);

        RecalculateSize();
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
        AddPlayerButton.transform.position = new Vector3(pos.x, pos.y, 0);
    }
}
