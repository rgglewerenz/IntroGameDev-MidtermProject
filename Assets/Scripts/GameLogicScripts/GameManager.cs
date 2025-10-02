using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Canvas PlayerUI;

    [SerializeField]
    public Canvas PauseMenuUI;

    [SerializeField]
    public Canvas GameOverUI;

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
        GameOverUI.gameObject.SetActive(true);
    }

}
