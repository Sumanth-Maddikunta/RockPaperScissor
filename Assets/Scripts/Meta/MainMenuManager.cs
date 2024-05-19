using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    private SceneHandler sceneHandler;

    public Button startGameButton;
    public TMP_Text highScore;

    private void Start()
    {
        sceneHandler = SceneHandler.GetInstance;
        startGameButton.onClick.AddListener(LoadGameScene);
        UpdateHighScoreText();
    }

    private void LoadGameScene()
    {
        sceneHandler.LoadGameScene();
    }

    private void UpdateHighScoreText()
    {
        if(PlayerPrefs.HasKey("HS"))
        {
            highScore.text = "Highscore : " + PlayerPrefs.GetInt("HS");
        }
    }
}
