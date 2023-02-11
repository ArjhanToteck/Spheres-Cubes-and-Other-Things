using UnityEngine;
using UnityEngine.UI;

public class HideText : MonoBehaviour
{
    public GameManager gameManager;
    public Text shownText;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && gameManager.gameEnded == false)
        {
            shownText.GetComponent<Animator>().SetTrigger("FadeOut");
        } else if (gameManager.gameEnded)
        {
            shownText.GetComponent<Animator>().SetTrigger("FadeOut");
        }

    }
}
