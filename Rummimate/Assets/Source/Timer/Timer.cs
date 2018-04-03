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

    public AudioSource EndTimeSound;
    public AudioSource BeepSound;

    private const string SAVE_TIME_KEY = "SaveTime";
    private float _savedTime = 0;
    private float _curTime = 0;

    private bool _isOn = false;
    public bool IsOn
    {
        private set
        {
            _isOn = value;
            Minutes.enabled = !value;
            Seconds.enabled = !value;
        }
        get
        {
            return _isOn;
        }
    }


    private void Awake()
    {
        StartStopButton.onClick.AddListener(StartStopHandler);
        RestartButton.onClick.AddListener(RestartHandler);
        Minutes.onEndEdit.AddListener(EndEditHandler);
        Seconds.onEndEdit.AddListener(EndEditHandler);

        IsOn = false;
        _savedTime = PlayerPrefs.GetFloat(SAVE_TIME_KEY);
        _curTime = _savedTime;
        SetTime(_curTime);
    }

    private void OnDestroy()
    {
        StartStopButton.onClick.RemoveListener(StartStopHandler);
        RestartButton.onClick.RemoveListener(RestartHandler);
        Minutes.onEndEdit.RemoveListener(EndEditHandler);
        Seconds.onEndEdit.RemoveListener(EndEditHandler);

        PlayerPrefs.SetFloat(SAVE_TIME_KEY, _savedTime);
    }

    private void StartStopHandler()
    {
        if(EndTimeSound.isPlaying)
        {
            TurnEndTimeSound(false);
        }
        else
        {
            IsOn = !IsOn;
        }
    }

    private void RestartHandler()
    {
        _curTime = _savedTime;
        IsOn = true;
        TurnEndTimeSound(false);
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
            float prev = _curTime;
            _curTime -= Time.deltaTime;

            CheckBeep(prev, _curTime, 4.5f);
            CheckBeep(prev, _curTime, 5);
            CheckBeep(prev, _curTime, 10);

            if (_curTime < 0)
            {
                _curTime = 0;
                IsOn = false;
                TurnEndTimeSound(true);
            }

            SetTime(_curTime);
        }
    }

    private void SetTime(float curTime)
    {
        int seconds = (int)curTime;
        int minutes = seconds / 60;
        seconds %= 60;

        Minutes.text = minutes.ToString();
        Seconds.text = seconds.ToString();
    }

    private void CheckBeep(float prev, float cur, float sec)
    {
        if(prev>=sec && sec >= cur)
        {
            BeepSound.Play();
        }
    }

    private void TurnEndTimeSound(bool on)
    {
        if(on)
        {
            EndTimeSound.Play();
        }
        else
        {
            EndTimeSound.Stop();
        }
    }
}
