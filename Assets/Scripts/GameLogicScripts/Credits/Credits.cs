using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private SceneHandler _sceneManager;

    private void Start()
    {
        _sceneManager = FindObjectOfType<SceneHandler>();
    }

    public void BackToStartScene()
    {
        _sceneManager.LoadSceneByType(SceneHandler.Scene.Start);
    }

}
