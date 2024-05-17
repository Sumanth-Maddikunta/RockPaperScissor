using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour
{
    private const string MAINMENU = "MainMenu";
    private const string GAME = "GAME";

    private static SceneHandler instance = null;
    public static SceneHandler GetInstance { get { return instance; } }
    

    public void Awake()
    {
        if(instance == null)
        {
            instance = new SceneHandler();
            DontDestroyOnLoad(this.gameObject);
        }        
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(GAME);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAINMENU);
    }
}
