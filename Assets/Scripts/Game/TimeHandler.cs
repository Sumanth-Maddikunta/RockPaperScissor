using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    public Slider timerSlider;
    private float roundTime;
    private float localTime;
    private bool stopTimer = false;
    private GameManager gameManager;


    public void SetupTimeHandler(float roundTime, GameManager gameManager)
    {
        timerSlider.maxValue = roundTime;
        this.roundTime = roundTime;
        this.gameManager = gameManager;
    }

    IEnumerator Timer()
    {
        while(stopTimer ==false)
        {
            localTime -= Time.deltaTime;

            yield return new WaitForSeconds(0.01f);

            if(localTime <= 0)
            {
                stopTimer = true;
                gameManager.TimeUp();
            }

            if(stopTimer == false)
            {
                timerSlider.value = localTime;
            }

        }
    }

    public void ResetTimer()
    {
        localTime = roundTime;
        timerSlider.value = roundTime;
        stopTimer = false;
        StartCoroutine(Timer());
    }
}
