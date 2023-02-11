using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LevelSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Text buttonText;
    int level;
    bool mouseOver = false;

    void Start()
    {
        buttonText = transform.GetChild(0).gameObject.GetComponent<Text>();
        level = Int32.Parse(buttonText.text);
    }

    void Update()
    {
        if (mouseOver)
        {
            buttonText.color = Color.yellow;
            buttonText.text = $"High Score: {FindObjectOfType<GameManager>().progress.levelScores[level - 1]}% \n Attempts: {FindObjectOfType<GameManager>().progress.levelAttempts[level - 1]}";
            buttonText.fontSize = 12;
        }
        else
        {
            buttonText.color = Color.white;
            buttonText.text = level.ToString();
            buttonText.fontSize = 50;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
