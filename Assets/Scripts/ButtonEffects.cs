using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Text buttonText;

    void Start()
    {
        buttonText = transform.GetChild(0).gameObject.GetComponent<Text>();
        GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<SoundManager>().select());
    }

    private bool mouseOver = false;
    void Update()
    {
        if (mouseOver)
        {
            buttonText.color = Color.yellow;
        } 
        else
        {
            buttonText.color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        FindObjectOfType<SoundManager>().select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
