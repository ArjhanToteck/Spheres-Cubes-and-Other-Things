using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Scrollbar music;
    public Scrollbar soundEffects;

    void Start()
    {
        // sets values of UI inputs
        soundEffects.value = FindObjectOfType<GameManager>().progress.sfxVolume;
        music.value = FindObjectOfType<GameManager>().progress.musicVolume;

        soundEffects.onValueChanged.AddListener((float val) => UpdateSettings());
        music.onValueChanged.AddListener((float val) => UpdateSettings());
    }

    // updates progress variable to UI inputs on change
    public void UpdateSettings()
    {
        FindObjectOfType<GameManager>().progress.sfxVolume = soundEffects.value;
        FindObjectOfType<GameManager>().progress.musicVolume = music.value;
        SavesManager.SaveProgress(FindObjectOfType<GameManager>().progress);
    }
}
