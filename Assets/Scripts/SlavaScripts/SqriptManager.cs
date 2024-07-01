using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public GameObject scriptTouch; // Reference to the GameObject with Script1
    public GameObject scriptGyro;

    void Start()
    {
        int canvasSelected = PlayerPrefs.GetInt("ButtonSelected", 0);

        // Disable specific scripts based on canvas selection
        if (canvasSelected == 1)
        {
            scriptTouch.SetActive(false);
            scriptGyro.SetActive(false);
        }
        if (canvasSelected == 2)
        {
            
            scriptTouch.SetActive(false);
        }
    }
}
