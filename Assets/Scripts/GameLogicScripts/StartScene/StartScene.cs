using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class StartScene : MonoBehaviour
{

    private SceneHandler _sceneManager;

    private void Start()
    {
        _sceneManager = FindObjectOfType<SceneHandler>();
    }

    public void StartGame()
    {
        _sceneManager.LoadSceneByType(SceneHandler.Scene.Game);
    }

    public void ShowHighScores()
    {
        _sceneManager.LoadSceneByType(SceneHandler.Scene.HighScores);
    }

    public void ShowCredits()
    {
        _sceneManager.LoadSceneByType(SceneHandler.Scene.Credits);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
