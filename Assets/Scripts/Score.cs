using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int correctAnswers = 0;
    public int questionsSeen = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void AddCorrectAnswer()
    {
        correctAnswers++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    public void AddQuestionsSeen()
    {
        questionsSeen++;
    }

    public int CalculateScore()
    {
        int currentScore = Mathf.RoundToInt(((float)correctAnswers / (float)questionsSeen) * 100);
        return currentScore;
    }
}
