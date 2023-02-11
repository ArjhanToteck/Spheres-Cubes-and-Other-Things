using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public bool gameEnded = false;
    public float restartDelay = 1f;
    public bool gameLost = false;

    // loads unity elements
    public GameObject levelCompleteUI;
    public GameObject levelFailedUI;
    public GameObject pausedUI;
    public Animator pausedAnim;
    public Text deathReason;
    public Text score;
    public Text level;
    public SaveData progress;
    public GameObject progressReset;

    void Awake()
    {
        progress = SavesManager.LoadProgress();
    }

    void Start()
    {
        Physics.gravity = new Vector3(0f, -9.81f, 0f);
        Time.timeScale = 1;

        // open the game achievement
        FindObjectOfType<AchievementManager>().UnlockAchievement("Open the Game", "Literally open the game.");

        bool allAttempted = true;
        bool hundredAttempts = false;

        // jack of all trades achievement
        for (int i = 0; i < progress.levelAttempts.Length; i++)
        {
            if (progress.levelAttempts[i] <= 0)
            {
                allAttempted = false;
            }

            if (progress.levelAttempts[i] >= 100)
            {
                hundredAttempts = true;
            }
        }

        if (allAttempted) FindObjectOfType<AchievementManager>().UnlockAchievement("Jack of All Trades", "Attempt every level at least once.");
        if (hundredAttempts) FindObjectOfType<AchievementManager>().UnlockAchievement("Practice Makes Perfect", "Attempt the same level 100 times.");
    }

    bool escPressed = false;

    void Update()
    {
        // keeps track of time
        progress.timePlayed += Time.unscaledDeltaTime;
        SavesManager.SaveProgress(progress);

        // go to sleep achievement
        DateTime currentTime = DateTime.Now;

        if(currentTime.Hour < 5) FindObjectOfType<AchievementManager>().UnlockAchievement("Go To Sleep", "Play the game before 5 AM.");

        // epic gamer achievement
        bool allLevelsWon = true;
        for(int i = 0; i < progress.levelScores.Length; i++)
        {
            if(progress.levelScores[i] < 100)
            {
                allLevelsWon = false;
            }
        }

        if(allLevelsWon) FindObjectOfType<AchievementManager>().UnlockAchievement("Epic Gamer", "Beat every level.");

        // 100% completion achievement
        bool allAchievements = true;
        for (int i = 0; i < progress.achievements.Length - 1; i++)
        {
            if (progress.achievements[i] == false)
            {
                allAchievements = false;
            }
        }

        if (allAchievements && allLevelsWon) FindObjectOfType<AchievementManager>().UnlockAchievement("100% Completion", "Get 100% completion.");

        // pauses game
        if (!!pausedUI)
        {
            // checkes if esc was pressed
            if (Input.GetKey(KeyCode.Escape))
            {
                escPressed = true;
            }

            // checks if esc was pressed but not anymore and game is not over
            if (!Input.GetKey(KeyCode.Escape) && escPressed && !gameEnded)
            {
                if (pausedUI.activeSelf)
                {
                    resumeGame();
                }
                else
                {
                    pauseGame();
                }

                escPressed = false;
            }

            // detects if pause panel has faded out
            if (pausedUI.GetComponent<Image>().color.a == 0 && pausedUI.activeSelf && pausedAnim.GetBool("Fade"))
            {
                pausedAnim.SetBool("Fade", false);
                Time.timeScale = 1;
                pausedUI.SetActive(false);
            }
        }        
    }

    public void GameOver(string deathMessage = "")
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            gameLost = true;

            // makes score and level red to make clear that game is over
            score.color = Color.red;
            level.color = Color.red;

            // disables movement
            FindObjectOfType<PlayerMovement>().enabled = false;

            // gets ready for death screen
            deathReason.text = $"{deathMessage} with a score of {score.text}.";

            // can we get an F? achievement
            FindObjectOfType<AchievementManager>().UnlockAchievement("Can we get an F?", "Die for the first time.");

            // bruh moment achievement
            if (Math.Round(FindObjectOfType<PercentageScore>().percentScore) <= 5f)
            {
                FindObjectOfType<AchievementManager>().UnlockAchievement("Bruh Moment", "Die with a score of 5% or less.");
            }

            // epic bruh moment achievement
            if (Math.Round(FindObjectOfType<PercentageScore>().percentScore) <= 1)
            {
                FindObjectOfType<AchievementManager>().UnlockAchievement("Epic Bruh Moment", "Die with a score of 1% or less.");
            }

            // nice achievement
            if (Math.Round(FindObjectOfType<PercentageScore>().percentScore) == 69)
            {
                FindObjectOfType<AchievementManager>().UnlockAchievement("Nice", "Die with a score of 69%.");
            }

            // pain achievement
            if (Math.Round(FindObjectOfType<PercentageScore>().percentScore) >= 95)
            {
                FindObjectOfType<AchievementManager>().UnlockAchievement("Pain", "Die with a score of 95% or more.");
            }

            // true pain achievement
            if (Math.Round(FindObjectOfType<PercentageScore>().percentScore) >= 99)
            {
                FindObjectOfType<AchievementManager>().UnlockAchievement("True Pain", "Die with a score of 99%.");
            }

            int highScoreIndex = Int32.Parse(SceneManager.GetActiveScene().name.Last().ToString()) - 1 == -1 ? 9 : Int32.Parse(SceneManager.GetActiveScene().name.Last().ToString()) - 1;

            if (progress.levelScores[highScoreIndex] < Math.Round(FindObjectOfType<PercentageScore>().percentScore))
            {
                progress.levelScores[highScoreIndex] = (int)Math.Round(FindObjectOfType<PercentageScore>().percentScore);
            }

            progress.levelAttempts[highScoreIndex]++;
            SavesManager.SaveProgress(progress);

            Invoke("gameOverScreen", restartDelay);
        }
    }

    public void gameOverScreen()
    {
        levelFailedUI.SetActive(true);
    }

    public void LevelComplete()
    {
        // makes score 100%
        FindObjectOfType<PercentageScore>().enabled = false;
        score.text = "100%";

        int highScoreIndex = Int32.Parse(SceneManager.GetActiveScene().name.Last().ToString()) - 1 == -1 ? 9 : Int32.Parse(SceneManager.GetActiveScene().name.Last().ToString()) - 1;
        progress.levelScores[highScoreIndex] = 100;

        gameEnded = true;

        
        if (progress.levelScores[highScoreIndex] < Math.Round(FindObjectOfType<PercentageScore>().percentScore))
        {
            progress.levelScores[highScoreIndex] = (int)Math.Round(FindObjectOfType<PercentageScore>().percentScore);
        }

        progress.levelAttempts[highScoreIndex]++;
        SavesManager.SaveProgress(progress);

        FindObjectOfType<PlayerMovement>().enabled = false;
        levelCompleteUI.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // resets time scale
        Time.timeScale = 1;
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pausedUI.SetActive(true);
    }

    public void resumeGame()
    {
        pausedAnim.SetBool("Fade", true);
    }

    public void resetProgress()
    {
        if (!progressReset.activeSelf)
        {
            progressReset.SetActive(true);
        } else
        {
            progress = new SaveData();
            SavesManager.SaveProgress(new SaveData());
            RestartLevel();
        }
        
    }

    public void openMenu()
    {
        // ad menu opening here
        SceneManager.LoadScene("Menu");

        // resets time scale
        Time.timeScale = 1;
    }

    public void openCredits()
    {
        // ad menu opening here
        SceneManager.LoadScene("Credits");
    }

    public void openAchievements()
    {
        // ad menu opening here
        SceneManager.LoadScene("Achievements");
    }

    public void openSettings()
    {
        // ad menu opening here
        SceneManager.LoadScene("Settings");
    }

    public void openLevelSelect()
    {
        // ad menu opening here
        SceneManager.LoadScene("LevelSelect");
    }

    public void closeApp()
    {
        Application.Quit();
    }
}
