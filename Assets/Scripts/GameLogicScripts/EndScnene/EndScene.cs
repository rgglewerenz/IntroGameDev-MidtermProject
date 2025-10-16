using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    [SerializeField]
    public TMPro.TMP_Text highScoresText;


    private HighScoreHandler highScoreHandler;
    private SceneHandler sceneHandler;


    void Start()
    {
        highScoreHandler = new HighScoreHandler();
        sceneHandler = FindObjectOfType<SceneHandler>();
        highScoresText.text = GetHighScores();
    }

    private string GetHighScores()
    {
        var scores = highScoreHandler.ScoreEntries;

        if(scores.Count == 0)
        {
            return "No high scores yet!";
        }

        scores = scores.OrderByDescending(x => x.Score).ToList();

        var result = "";

        for(int i = 0; i < scores.Count && i < 5; i++)
        {
            result += $"{i + 1}) {scores[i].PlayerName} - {scores[i].Score}\n";
        }

        return result;
    }

    public void GoBackToStart()
    {
        sceneHandler.LoadSceneByType(SceneHandler.Scene.Start);
    }

    public void StartGame()
    {
        sceneHandler.LoadSceneByType(SceneHandler.Scene.Game);
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


