using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    Score score;

    private void Awake()
    {
        score = FindObjectOfType<Score>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulation!\n You scored: " + score.CalculateScore() + "%";
    } 
}
