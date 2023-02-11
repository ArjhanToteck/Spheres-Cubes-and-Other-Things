using System;
using UnityEngine;
using UnityEngine.UI;

public class PercentageScore : MonoBehaviour
{
    // loads Unity objects
    public Transform player;
    public Transform LevelEnd;
    public Text score;
    public float percentScore;

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameManager>().gameEnded == false)
        {
            percentScore = player.position.z / LevelEnd.position.z * 100 > 100 ? 100f : player.position.z / LevelEnd.position.z * 100;

            if((FindObjectOfType<GameManager>().gameLost || FindObjectOfType<GameManager>().gameEnded == false) && Math.Round(percentScore) >= 100)
            {
                percentScore = 99;
            }

            score.text = $"{Math.Round(percentScore)}%";
        }
    }
}
