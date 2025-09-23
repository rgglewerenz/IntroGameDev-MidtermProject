using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField]
    public TMPro.TMP_Text scoreText;

    float score = 0;


    public void UpdateScore(float pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
