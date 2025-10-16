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

    private ScoreHandler scoreHandler;

    private SceneHandler sceneHandler;

    private HighScoreHandler highScoreHandler;


    void Start()
    {
        highScoreHandler = new HighScoreHandler();
        scoreHandler = FindObjectOfType<ScoreHandler>();
        sceneHandler = FindObjectOfType<SceneHandler>();
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


    public void MoveToStartGame()
    {
        GameOverUI.gameObject.SetActive(false);
        Resume();
        sceneHandler.LoadSceneByType(SceneHandler.Scene.Start);
    }

    public void GameOver()
    {
        Pause();
        PlayerUI.gameObject.SetActive(false);
        PauseMenuUI.gameObject.SetActive(false);

        if(highScoreHandler.ScoreEntries.Count == 0 || scoreHandler.Score >= highScoreHandler.ScoreEntries[highScoreHandler.ScoreEntries.Count].Score)
        {
            GameOverHighScoreUI.gameObject.SetActive(true);
            return;
        }


        GameOverUI.gameObject.SetActive(true);
    }

    public void SaveHighScore()
    {
        var score = scoreHandler.Score;
        var name = NameInputField.text;

        if (string.IsNullOrEmpty(name) || name.Contains(","))
        {
            errorText.gameObject.SetActive(true);
            return;
        }

        highScoreHandler.AddScore(name, score);

        MoveToStartGame();
    }


    public void ResumeGame()
    {
        PauseMenuUI.gameObject.SetActive(false);
        PlayerUI.gameObject.SetActive(true);
        Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            PauseGame();
        }
    }
}
