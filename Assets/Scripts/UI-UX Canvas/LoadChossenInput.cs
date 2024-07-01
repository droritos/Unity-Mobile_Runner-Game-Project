using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadChossenInput : MonoBehaviour
{
    private void Awake()
    {
        int option = PlayerPrefs.GetInt("InputSetting");

        if (option == 0) // Swing Option
        {
            
        }
        else if (option == 1) // Buttons
        {

        }
    }
}
