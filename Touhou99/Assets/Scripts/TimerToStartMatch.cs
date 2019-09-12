using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerToStartMatch : MonoBehaviour
{
    bool canCountDown = false;
    bool matchHasStarted = false;
    float timerToStart = 3f;
    [HideInInspector] public float countDownToStart;

    void Start()
    {
        countDownToStart = timerToStart;
    }

    void Update()
    {
        if(canCountDown == true)
        {
            if (countDownToStart > 0)
            {
                countDownToStart -= Time.deltaTime;
            }

            else if (countDownToStart <= 0)
            {
                matchHasStarted = true;
            }
        }        
    }

    public void StartCanCountDown()
    {
        canCountDown = true;
    }
}
