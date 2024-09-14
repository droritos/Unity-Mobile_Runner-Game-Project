using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For accessing the UI elements like Button and Text
using System.IO;
using System.Collections;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playButtonText; // Reference to the Text component of the Play button
    [SerializeField] PlayerStatsConfig playerStatsConfigHolder;
    private bool _hasSaveFile; // Boolean to track if a save file exists


    private void Start()
    {
        //UpdatePlayButtonText();
    }


    #region << Scenes Movers>>
    public void PlayTheGameButton()
    {
        SceneManager.LoadScene(2);
    }
    public void OpenSetingsMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ShopMenu()
    {
        SceneManager.LoadScene(4);
    }
    #endregion

    #region << Touch Settings >>
    public void SetInputButtons()
    {
        PlayerPrefs.SetInt("ButtonSelected", 1);
        Input.gyro.enabled = false;
        Debug.Log("buttons");
    }
    public void SetInputTouch()
    {
        PlayerPrefs.SetInt("ButtonSelected", 0);
        Input.gyro.enabled = false;
        Debug.Log("touch");
    }
    public void SetInputGyro()
    {
        PlayerPrefs.SetInt("ButtonSelected", 2);
        Input.gyro.enabled = true;
        Debug.Log("Gyroo");
    }
    #endregion
    public void MenuSwitcher(GameObject menu)
    {
        if (menu.activeInHierarchy)
            menu.SetActive(false);
        else
            menu.SetActive(true);
    }

    #region << Continue Game >>
    private void CheckForSaveFile()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "Save In Game Data.game");
        Debug.Log($"Checking for save file at: {saveFilePath}");
        _hasSaveFile = File.Exists(saveFilePath); // Set the boolean based on whether the file exists
        Debug.Log($"Found Saved Data : {_hasSaveFile}");
    }

    private void UpdatePlayButtonText()
    {
        if (playButtonText != null)
        {
            playButtonText.text = "Continue";
        }
        else if (playButtonText != null)
        {
            playButtonText.text = "New Game";
        }
    }



    #endregion
}
