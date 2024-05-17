using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private SceneHandler sceneHandler;

    public Button startGameButton;

    private void Start()
    {
        sceneHandler = SceneHandler.GetInstance;
        startGameButton.onClick.AddListener(LoadGameScene);
    }

    private void LoadGameScene()
    {
        sceneHandler.LoadGameScene();
    }
}
