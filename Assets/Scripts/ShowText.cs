using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public GameManager gameManager;
    public Text shownText;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && gameManager.gameEnded == false)
        {
            shownText.gameObject.SetActive(true);
        }

    }
}
