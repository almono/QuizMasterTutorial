using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToComplete = 5f, timeToShowAnswer = 2.5f;
    private float timerValue;
    public bool isAnswering;
    public bool loadNextQuestion;
    public float fillAmount;

    void Update()
    {
        UpdateTimer();
    }

    // for when question is clicked
    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(isAnswering)
        {
            if(timerValue > 0 )
            {
                fillAmount = timerValue / timeToComplete;
            } else 
            {
                isAnswering = false;
                timerValue = timeToShowAnswer;
            }
        } else
        {
            if (timerValue > 0)
            {
                fillAmount = timerValue / timeToShowAnswer;
            } else {
                isAnswering = true;
                timerValue = timeToComplete;
                loadNextQuestion = true;
            }
        }
    }
}
