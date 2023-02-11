using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowSelect : MonoBehaviour
{
    public List<GameObject> options = new List<GameObject>();
    public GameObject selectedOption;
    public float arrowSpacing = 20f;

    public int selectedIndex
    {
        get { return SelectedIndex; }
        set
        {
            SelectedIndex = value;
            UpdateOptions();
        }
    }

    int SelectedIndex = 0;

    void Start()
    {
       UpdateOptions();
    }

    public void UpdateOptions()
    {
        // recounts children if neccessary
        if(options.Count != transform.GetChild(0).childCount)
        {
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                options.Add(transform.GetChild(0).GetChild(i).gameObject);
            }
        }
        
        // disables all unselected options
        for (int i = 0; i < options.Count; i++)
        {
            if(i == selectedIndex)
            {
                options[i].SetActive(true);
                selectedOption = options[i];
            } else
            {
                options[i].SetActive(false);
            }
        }
    }

    public void NextOption()
    {
        selectedIndex++;

        if (selectedIndex >= options.Count) selectedIndex = 0;
    }

    public void PreviousOption()
    {
        selectedIndex--;

        if (selectedIndex < 0) selectedIndex = options.Count - 1;
    }
}
