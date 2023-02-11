using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public GameManager gameManager;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && gameManager.gameEnded == false)
        {
            // executes LevelComplete function in GameManager
            gameManager.LevelComplete();
        }
        
    }
}
