using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Answers")]
    [SerializeField] private GameObject[] answerButtons = new GameObject[4];
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultAnswerSprite, correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] private Image timerImage;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        questionText.text = question.GetQuestion();
        DisplayQuestion();       
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillAmount;

        if(timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if(!hasAnsweredEarly && !timer.isAnswering)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void DisplayAnswer(int btnIndex)
    {
        Image btnImage;

        if (btnIndex == question.GetCorrectAnswer())
        {
            questionText.text = "Correct";
            btnImage = answerButtons[btnIndex].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
        }
        else
        {
            int answerIndex = question.GetCorrectAnswer();
            questionText.text = "Sorry but the correct answer is: \n" + question.GetAnswer(answerIndex);

            btnImage = answerButtons[answerIndex].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
        }
    }

    public void OnAnswerSelected(int btnIndex)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(btnIndex);
        SetButtonState(false);
        timer.CancelTimer();
    }

    public void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprite();
        DisplayQuestion();
    }

    public void DisplayQuestion()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    // For disabling/enabling interactions with buttons
    public void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    public void SetDefaultButtonSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image btnImage = answerButtons[i].GetComponent<Image>();
            btnImage.sprite = defaultAnswerSprite;
        }
    }
}
