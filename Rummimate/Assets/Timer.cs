using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour {

    public Button StartStopButton;
    public Button RestartButton;
    public TMP_InputField Minutes;
    public TMP_InputField Seconds;

    private float _savedTime = 0;
    private float _curTime = 0;
    public bool IsOn { private set; get; }

    private void Awake()
    {
        StartStopButton.onClick.AddListener(StartStopHandler);
        RestartButton.onClick.AddListener(RestartHandler);
        Minutes.onEndEdit.AddListener(EndEditHandler);
        Seconds.onEndEdit.AddListener(EndEditHandler);

        IsOn = false;
    }

    private void OnDestroy()
    {
        StartStopButton.onClick.RemoveListener(StartStopHandler);
        RestartButton.onClick.RemoveListener(RestartHandler);
        Minutes.onEndEdit.RemoveListener(EndEditHandler);
        Seconds.onEndEdit.RemoveListener(EndEditHandler);
    }

    private void StartStopHandler()
    {
        IsOn = !IsOn;
    }

    private void RestartHandler()
    {
        _curTime = _savedTime;
        IsOn = true;
    }

    private void EndEditHandler(string str)
    {
        int minutes;
        int.TryParse(Minutes.text, out minutes);

        int seconds;
        int.TryParse(Seconds.text, out seconds);

        minutes %= 60;
        seconds %= 60;

        _savedTime = minutes * 60 + seconds;
        _curTime = _savedTime;
    }

    private void Update()
    {
        if(IsOn)
        {
            _curTime -= Time.deltaTime;
            if(_curTime < 0)
            {
                _curTime = 0;
                IsOn = false;
                PlayEndTimeSound();
            }

            int seconds = (int)_curTime;
            int minutes = seconds / 60;
            seconds %= 60;

            Minutes.text = minutes.ToString();
            Seconds.text = seconds.ToString();
        }
    }

    private void PlayEndTimeSound()
    {
        
    }
}
