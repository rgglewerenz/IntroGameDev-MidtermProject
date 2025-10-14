using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField]
    public TMPro.TMP_Text scoreText;

    int score = 0;

    public int Score { get => score; }


    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
