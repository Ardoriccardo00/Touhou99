using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    void Countdown(float seconds)
    {
        seconds -= Time.deltaTime;
        if (seconds <= 0)
            Tick();
    }

    void Tick()
    {

    }
}
