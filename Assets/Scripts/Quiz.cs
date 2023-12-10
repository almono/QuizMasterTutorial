using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO currentQuestion;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] private List<QuestionSO> questionList = new List<QuestionSO>();

    [Header("Answers")]
    [SerializeField] private GameObject[] answerButtons = new GameObject[4];
    bool hasAnsweredEarly = true;

    [Header("Button Sprites")]
    [SerializeField] private Sprite defaultAnswerSprite, correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] private Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    Score score;

    [Header("Progress")]
    [SerializeField] Slider progressSlider;

    public bool isComplete;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        score = FindObjectOfType<Score>();
        progressSlider = FindObjectOfType<Slider>();
        progressSlider.maxValue = questionList.Count;
        
        //questionText.text = currentQuestion.GetQuestion();
        //DisplayQuestion();
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

        if (btnIndex == currentQuestion.GetCorrectAnswer())
        {
            questionText.text = "Correct";
            btnImage = answerButtons[btnIndex].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
            score.AddCorrectAnswer();
        }
        else
        {
            int answerIndex = currentQuestion.GetCorrectAnswer();
            questionText.text = "Sorry but the correct answer is: \n" + currentQuestion.GetAnswer(answerIndex);

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
        scoreText.text = "Score: " + score.CalculateScore() + "%";

        if(progressSlider.value == progressSlider.maxValue)
        {
            isComplete = true;
        }
    }

    public void GetNextQuestion()
    {
        if(questionList.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            score.AddQuestionsSeen();
            progressSlider.value++;
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questionList.Count);
        currentQuestion = questionList[index];

        // after picking the question we remove it from list
        if(questionList.Contains(currentQuestion))
        {
            questionList.Remove(currentQuestion);
        }
    }

    public void DisplayQuestion()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }

        questionText.text = currentQuestion.GetQuestion();
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
