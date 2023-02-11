using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int levelsBeaten = 0;
    public int levelAttempts = 0;
    public int achievementsEarned = 0;
    public float timePlayed = 0f;
    public float completionPercent = 0f;

    List<string> unlockedAchievements = new List<string>();

    void Start()
    {
        // levels beaten
        for (int i = 0; i < FindObjectOfType<GameManager>().progress.levelScores.Length; i++)
        {
            if(FindObjectOfType<GameManager>().progress.levelScores[i] == 100)
            {
                levelsBeaten++;
            }   
        }

        levelAttempts = FindObjectOfType<GameManager>().progress.levelAttempts.Sum();
        timePlayed = (float)Math.Round(FindObjectOfType<GameManager>().progress.timePlayed / 60);

        // achievements
        for (int i = 0; i < FindObjectOfType<GameManager>().progress.achievements.Length; i++)
        {
            if (FindObjectOfType<GameManager>().progress.achievements[i])
            {
                unlockedAchievements.Add(FindObjectOfType<AchievementManager>().achievements[i]);
            }
        }

        achievementsEarned = FindObjectOfType<AchievementDisplay>().unlockedAchievements.Count;

        completionPercent = (float)Math.Round((levelsBeaten * 8) + (achievementsEarned * 20 / FindObjectOfType<AchievementManager>().achievements.Length + 0f));

        GetComponent<Text>().text = $"Completion Percent: {completionPercent}% \n Achievements Earned: {achievementsEarned}/{FindObjectOfType<AchievementManager>().achievements.Length} \n Levels Beaten: {levelsBeaten}/10 \n Time Played: {timePlayed} minutes \n Total Level Attempts: {levelAttempts}";
    }
}
