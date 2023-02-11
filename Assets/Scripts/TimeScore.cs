using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeScore : MonoBehaviour
{
    // timer settings
    public float timeRemaining;
    public bool timerIsRunning = false;

    // loads Unity objects
    public Text score;

    void Update()
    {
        // runs timer
        if (timerIsRunning && FindObjectOfType<GameManager>().gameEnded == false)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                FindObjectOfType<GameManager>().GameOver(" You ran out of time!");
            }
        }

        // displays timer
        if(timerIsRunning)
        {
            score.text = $"{Math.Round(timeRemaining)}s";
        }
    }
}
