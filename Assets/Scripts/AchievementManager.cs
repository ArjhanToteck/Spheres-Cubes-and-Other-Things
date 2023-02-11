using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public Text achievementName;
    public Text achievementDesc;
    public GameObject achievementPanel;
    public Animator background;
    public Animator background1;
    bool faded = false;
    bool achievementOpen = false;
    public string[] achievements;
    public string[] achievementDescs;

    List<string[]> achievementQueue = new List<string[]>();

    public void UnlockAchievement(string name, string desc, bool overrideLock = false)
    {
        if(FindObjectOfType<GameManager>().progress.achievements[Array.IndexOf(achievements, name.ToUpper())] == false || overrideLock)
        {
            if (!achievementOpen)
            {
                achievementOpen = true;
                achievementName.text = name.ToUpper();
                achievementDesc.text = desc;
                achievementPanel.SetActive(true);
                FindObjectOfType<SoundManager>().achievement();
                FindObjectOfType<GameManager>().progress.achievements[Array.IndexOf(achievements, name.ToUpper())] = true;

                Invoke("fadeAchievement", 3f);
            }
            else
            {
                achievementQueue.Add(new string[] { name, desc });
                FindObjectOfType<GameManager>().progress.achievements[Array.IndexOf(achievements, name.ToUpper())] = true;
            }
        }
    }

    void fadeAchievement()
    {
        background.SetBool("Fade", true);
        background1.SetBool("Fade", true);
        faded = true;
    }

    void Awake()
    {
        achievements = new string[] { "OPEN THE GAME", "GO TO SLEEP", "CAN WE GET AN F?", "NICE", "BRUH MOMENT", "EPIC BRUH MOMENT", "PAIN", "TRUE PAIN", "JACK OF ALL TRADES", "PRACTICE MAKES PERFECT", "EPIC GAMER", "100% COMPLETION" };
        achievementDescs = new string[] { "Literally open the game.", "Play the game before 5 AM.", "Die for the first time.", "Die with a score of 69%.", "Die with a score of 5% or less.", "Die with a score of 1% or less.", "Die with a score of 95% or more.", "Die with a score of 99%.", "Attempt every level at least once.", "Attempt the same level 100 times.", "Beat every level.", "Get 100% completion." };
    }

    void Update()
    {
        if(faded && background.gameObject.GetComponent<Image>().color.a == 0 && background1.gameObject.GetComponent<Image>().color.a == 0)
        {
            faded = false;
            achievementOpen = false;
            achievementPanel.SetActive(false);
        }

        if (!achievementOpen && achievementQueue.Count > 0)
        {
            UnlockAchievement(achievementQueue[0][0], achievementQueue[0][1], true);
            achievementQueue.Remove(achievementQueue[0]);
        }
    }
}
