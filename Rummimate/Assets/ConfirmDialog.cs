using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmData
{
    public string Message;
    public Action<string> OnConfirm;
}

public class ConfirmDialog : MonoBehaviour {
    public Button CloseButton;
    public Button ConfirmButton;
    public TextMeshProUGUI Message;
    public TMP_InputField Answer;

    private ConfirmData _data;

    private void Awake()
    {
        CloseButton.onClick.AddListener(CloseHandler);
        ConfirmButton.onClick.AddListener(ConfirmHandler);
    }

    private void OnDestroy()
    {
        CloseButton.onClick.RemoveListener(CloseHandler);
        ConfirmButton.onClick.RemoveListener(ConfirmHandler);
    }

    private void ConfirmHandler()
    {
        string answer = Answer.text;
        _data.OnConfirm(answer);
        Close();
    }

	private void CloseHandler()
    {
        Close();
    }

    public void Open(ConfirmData data)
    {
        _data = data;
        Message.text = data.Message;
        Answer.text = "";
        gameObject.SetActive(true);
    }

	private void Close()
    {
        gameObject.SetActive(false);
    }
}
