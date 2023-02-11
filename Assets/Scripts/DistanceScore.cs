using System;
using UnityEngine;
using UnityEngine.UI;

public class DistanceScore : MonoBehaviour
{
    // loads Unity objects
    public Transform player;
    public Text score;

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameManager>().gameEnded == false)
        {
            score.text = $"{Math.Round(player.position.z)}m";
        }
    }
}
