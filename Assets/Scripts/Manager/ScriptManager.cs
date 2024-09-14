using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public GameObject scriptTouch; // Reference to the GameObject with Script1
    public GameObject scriptGyro;
    [SerializeField] GameObject MovementButtons;

    void Awake()
    {
        DisableScripts();
        DisableButtonsMenu();
        DecideInput();
    }

    private void DecideInput()
    {
        int canvasSelected = PlayerPrefs.GetInt("ButtonSelected"); // Note - 0 is the deafult Input Settings
        // Disable specific scripts based on canvas selection
        switch (canvasSelected)
        {
            case 0:
                scriptTouch.SetActive(true);
                break;
            case 1:
                MovementButtons.SetActive(true);
                break;
            case 2:
                scriptGyro.SetActive(true);
                break;
            default:
                scriptTouch.SetActive(true);
                break;
        }
        Debug.Log($"Input Type Chosen {canvasSelected}");
    }

    private void DisableScripts()
    {
        scriptGyro.SetActive(false);
        scriptTouch.SetActive(false);
    }
    private void DisableButtonsMenu()
    {
        MovementButtons.SetActive(false);
    }

}
