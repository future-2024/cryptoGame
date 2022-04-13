using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    //This script should be attached to a UI Text element
    public Text timerText;
    public float miliSecond;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;

    void Update()
    {
        UpdateTimerUI();
    }
    //call this on update
    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        miliSecond += Time.deltaTime;
        timerText.text = "Delay Time:" + hourCount + "h:" + minuteCount + "m:" + (int)secondsCount + "s:" + (int)miliSecond + "j";
        Debug.Log(timerText.text);
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            hourCount++;
            minuteCount = 0;
        }
    }
}