using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public enum Scene
    {
        Start,
        Game,
        HighScores
    }

    private readonly Dictionary<Scene, string> _sceneNames = new Dictionary<Scene, string>()
    {
        { Scene.Start, "Start" },
        { Scene.Game,  "Game"},
        { Scene.HighScores,  "HighScores"}
    };


    public void LoadSceneByType(Scene scene)
    {
        SceneManager.LoadScene(_sceneNames[scene]);
    }
}
        