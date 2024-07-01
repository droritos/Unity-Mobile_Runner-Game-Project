using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoseInput : MonoBehaviour
{
    public GameObject canvasUI;
    // Start is called before the first frame update
    void Start()
    {
        // Check if the canvas is selected (from PlayerPrefs or other method)
        bool canvasSelected = PlayerPrefs.GetInt("ButtonSelected", 0) == 1;

        // Enable or disable the canvas UI based on selection
        canvasUI.SetActive(canvasSelected);
    }
}
