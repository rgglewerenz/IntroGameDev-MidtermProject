using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Canvas PlayerUI;

    [SerializeField]
    public Canvas PauseMenuUI;

    [SerializeField]
    public Canvas GameOverUI;

    [SerializeField]
    public Canvas GameOverHighScoreUI;

    [SerializeField]
    public TMP_InputField NameInputField;

    [SerializeField]
    public TMPro.TMP_Text errorText;

    List<int> HighScores = new List<int>();

    private string SCORE_FILE_PATH;

    private ScoreHandler scoreHandler;

    void Start()
    {
        if(!Directory.Exists(UnityEngine.Application.dataPath + "appdata"))
        {
            Directory.CreateDirectory(UnityEngine.Application.dataPath + "appdata");
        }
        if(!File.Exists(UnityEngine.Application.dataPath + "appdata/scores.txt"))
        {
            File.Create(UnityEngine.Application.dataPath + "appdata/scores.txt").Dispose();
        }

        SCORE_FILE_PATH  = UnityEngine.Application.dataPath + "appdata/scores.txt";

        HighScores = GetHighScores();
        scoreHandler = FindObjectOfType<ScoreHandler>();
    }


    public void PauseGame()
    {
        if(GameOverUI.gameObject.activeSelf) { return; }
        if (PauseMenuUI.gameObject.activeSelf)
        {
            Resume();
            PauseMenuUI.gameObject.SetActive(false);
            PlayerUI.gameObject.SetActive(true);
        }
        else
        {
            Pause();
            PauseMenuUI.gameObject.SetActive(true);
            PlayerUI.gameObject.SetActive(false);
        }
    }


    private void Pause()
    {
        Time.timeScale = 0f;
    }


    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


    public void RestartGame()
    {
        GameOverUI.gameObject.SetActive(false);
        Resume();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        Pause();
        PlayerUI.gameObject.SetActive(false);
        PauseMenuUI.gameObject.SetActive(false);

        if(HighScores.Count == 0 || scoreHandler.Score >= HighScores[HighScores.Count])
        {
            GameOverHighScoreUI.gameObject.SetActive(true);
            return;
        }


        GameOverUI.gameObject.SetActive(true);
    }


    private List<int> GetHighScores()
    {
        var lines = File.ReadAllLines(SCORE_FILE_PATH);
        var scores = new List<int>();
        foreach (var line in lines)
        {
            
            var items = line.Split(',');
            var score = int.Parse(items[0]);
            scores.Add(score);
        }
        scores.Sort();
        return scores;
    }


    public void SaveHighScore()
    {
        var score = scoreHandler.Score;
        var name = NameInputField.text;

        if (string.IsNullOrEmpty(name))
        {
            errorText.gameObject.SetActive(true);
            return;
        }


        using (StreamWriter sw = File.AppendText(SCORE_FILE_PATH))
        {
            sw.WriteLine(score + "," + name);
        }

        RestartGame();
    }

}
