using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip explosionSound;
    public AudioClip powerUpSound;
    public AudioClip bounceSound;
    public AudioClip achievementSound;
    public AudioClip titleMusicSound;
    public AudioClip selectSound;

    void Start()
    {
        if(!!titleMusicSound)
        {
            titleMusic();
        }
    }

    public void explosion()
    {
        audioSource.PlayOneShot(explosionSound, FindObjectOfType<GameManager>().progress.sfxVolume);
    }

    public void powerUp()
    {
        audioSource.PlayOneShot(powerUpSound, FindObjectOfType<GameManager>().progress.sfxVolume);
    }

    public void bounce()
    {
        audioSource.PlayOneShot(bounceSound, FindObjectOfType<GameManager>().progress.sfxVolume);
    }

    public void achievement()
    {
        audioSource.PlayOneShot(achievementSound, FindObjectOfType<GameManager>().progress.sfxVolume);
    }

    public void select()
    {
        audioSource.PlayOneShot(selectSound, FindObjectOfType<GameManager>().progress.sfxVolume);
    }

    public void titleMusic()
    {
        audioSource.clip = titleMusicSound;
        audioSource.loop = true;
        audioSource.volume = FindObjectOfType<GameManager>().progress.musicVolume;
        audioSource.Play();
    }

    // updates volume of background music
    void Update()
    {
        if(audioSource.volume != FindObjectOfType<GameManager>().progress.musicVolume)
        {
            audioSource.volume = FindObjectOfType<GameManager>().progress.musicVolume;
        }
    }
}
