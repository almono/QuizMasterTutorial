using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] private string question = "Enter new question";
    [SerializeField] private string[] answers = new string[4];
    [SerializeField] [Range(0,3)] private int correctAnswer;

    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswer() 
    {
        return correctAnswer;
    }

    public string GetAnswer(int answerIndex)
    {
        return answers[answerIndex];
    }
}
