using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {
    public Button OnOffButton;
    public GameObject ScenesList;
    public Button[] ScenesButtons;
    private readonly string[] _sceneNames = { "ScoreScene", "TimerScene", "HintScene" };

    private void Awake()
    {
        AddListeners();    
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        OnOffButton.onClick.AddListener(OnOffHandler);
        for(int i = 0; i < ScenesButtons.Length; i++ )
        {
            ScenesButtons[i].onClick.AddListener(LoadSceneAction(i));
        }
    }

    private void RemoveListeners()
    {
        OnOffButton.onClick.RemoveListener(OnOffHandler);
        for ( int i = 0; i < ScenesButtons.Length; i++ )
        {
            ScenesButtons[i].onClick.RemoveListener(LoadSceneAction(i));
        }
    }

    private void OnOffHandler()
    {
        ScenesList.SetActive(!ScenesList.activeSelf);
    }

    private UnityEngine.Events.UnityAction LoadSceneAction(int sceneNum)
    {
        return () => LoadScene(sceneNum);
    }

    private void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(_sceneNames[sceneNum], LoadSceneMode.Single);
    }
}
