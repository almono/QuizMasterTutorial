using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToComplete = 10f, timeToShowAnswer = 5f;
    private float timerValue;
    public bool isAnswering = false, loadNextQuestion = true;
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
                timerValue = timeToShowAnswer;
                isAnswering = false;
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
