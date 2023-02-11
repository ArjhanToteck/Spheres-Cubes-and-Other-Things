using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    public List<string> unlockedAchievements = new List<string>();
    public List<string> lockedAchievements = new List<string>();
    public int achievementIndex;

    // Start is called before the first frame update
    void Start()
    {
        // achievements
        FindObjectOfType<GameManager>().progress.achievements = SavesManager.LoadProgress().achievements;

        // gets locked and unlocked achievements
        for (int i = 0; i < FindObjectOfType<GameManager>().progress.achievements.Length; i++)
        {
            if (FindObjectOfType<GameManager>().progress.achievements[i])
            {
                unlockedAchievements.Add(FindObjectOfType<AchievementManager>().achievements[i]);
            }
            else
            {
                lockedAchievements.Add(FindObjectOfType<AchievementManager>().achievements[i]);
            }
        }

        unlockedAchievements.Sort();
        lockedAchievements.Sort();
                
        // unlocked achievements
        for (achievementIndex = 0; achievementIndex < unlockedAchievements.Count; achievementIndex++)
        {
            // copies achievement template and sets active
            GameObject achievement = Instantiate(transform.GetChild(0).gameObject);
            achievement.SetActive(true);

            // sets parent of copied object
            achievement.transform.SetParent(gameObject.transform);
            achievement.transform.localScale = new Vector3(1, 1, 1);

            // sets position of copied object
            achievement.transform.position = new Vector3(transform.GetChild(achievementIndex).position.x, transform.GetChild(0).position.y - (achievementIndex * (Screen.height / 4)), 0);

            // sets background color
            achievement.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.44f);

            // sets title
            achievement.transform.GetChild(1).gameObject.GetComponent<Text>().text = unlockedAchievements[achievementIndex];

            // sets desc
            achievement.transform.GetChild(2).gameObject.GetComponent<Text>().text = FindObjectOfType<AchievementManager>().achievementDescs[Array.IndexOf(FindObjectOfType<AchievementManager>().achievements, unlockedAchievements[achievementIndex])];
        }

        // locked achievements
        for (achievementIndex += 0; achievementIndex < lockedAchievements.Count + unlockedAchievements.Count; achievementIndex++)
        {
            // copies achievement template and sets active
            GameObject achievement = Instantiate(transform.GetChild(0).gameObject);
            achievement.SetActive(true);

            // sets parent of copied object
            achievement.transform.SetParent(gameObject.transform);
            achievement.transform.localScale = new Vector3(1, 1, 1); 

            // sets position of copied object
            achievement.transform.position = new Vector3(transform.GetChild(achievementIndex).position.x, transform.GetChild(0).position.y - (achievementIndex * (Screen.height / 4)), 0);

            // sets background color
            achievement.transform.GetChild(0).GetComponent<Image>().color = new Color(0.48f, 0.48f, 0.48f, 0.44f);

            // sets title
            achievement.transform.GetChild(1).gameObject.GetComponent<Text>().text = $"{lockedAchievements[Math.Abs(achievementIndex - unlockedAchievements.Count)]} (LOCKED)";

            // sets desc
            achievement.transform.GetChild(2).gameObject.GetComponent<Text>().text = FindObjectOfType<AchievementManager>().achievementDescs[Array.IndexOf(FindObjectOfType<AchievementManager>().achievements, lockedAchievements[Math.Abs(achievementIndex - unlockedAchievements.Count)])];
        }
    }
}
