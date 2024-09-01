using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For accessing the UI elements like Button and Text
using System.IO;
using System.Collections;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject sceneStateManagerPrefab; // Assign the prefab in the Inspector
    [SerializeField] TextMeshProUGUI playButtonText; // Reference to the Text component of the Play button

    private bool _hasSaveFile; // Boolean to track if a save file exists
    private SceneStateManager _sceneStateManager;

    private void Start()
    {
        // Check if the SceneStateManager already exists in the scene
        _sceneStateManager = FindObjectOfType<SceneStateManager>();

        if (_sceneStateManager == null)
        {
            // If it doesn't exist, instantiate it from the prefab
            GameObject manager = Instantiate(sceneStateManagerPrefab);
            _sceneStateManager = manager.GetComponent<SceneStateManager>();
            DontDestroyOnLoad(manager); // Prevent the SceneStateManager from being destroyed on scene load
            Debug.Log("SceneStateManager created and set to DontDestroyOnLoad.");
        }

        // Check for existing save file and update the button text
        CheckForSaveFile();
        UpdatePlayButtonText();
    }

    private void CheckForSaveFile()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "checkpoint_save.json");
        Debug.Log($"Checking for save file at: {saveFilePath}");
        _hasSaveFile = File.Exists(saveFilePath); // Set the boolean based on whether the file exists
        Debug.Log($"Found Saved Data : {_hasSaveFile}");
    }

    private void UpdatePlayButtonText()
    {
        if (_hasSaveFile && playButtonText != null)
        {
            playButtonText.text = "Continue Game";
        }
        else if (!_hasSaveFile && playButtonText != null)
        {
            playButtonText.text = "Start Game";
        }
    }

    public void PlayTheGameButton()
    {
        if (_hasSaveFile)
        {
            // If the save file exists, load the saved game state
            SceneManager.LoadScene(2); // Load the game scene
            StartCoroutine(ContinueGameAfterSceneLoad());
        }
        else
        {
            // If no save file exists, start a new game
            SceneManager.LoadScene(2);
            Debug.Log("New Game");
        }
    }

    private IEnumerator ContinueGameAfterSceneLoad()
    {
        yield return new WaitForSeconds(1); // Adjust timing as needed

        if (_sceneStateManager != null)
        {
            string saveFilePath = Path.Combine(Application.persistentDataPath, "checkpoint_save.json");
            _sceneStateManager.LoadSceneState(saveFilePath);
            Debug.Log("Game continued from saved state.");
        }
        else
        {
            Debug.LogError("SceneStateManager not found in the loaded scene.");
        }
    }

    public void SetInputButtons()
    {
        PlayerPrefs.SetInt("ButtonSelected", 1);
        Debug.Log("buttons");
    }

    public void SetInputTouch()
    {
        PlayerPrefs.SetInt("ButtonSelected", 0);
        Debug.Log("touch");
    }

    public void SetInputGyro()
    {
        PlayerPrefs.SetInt("ButtonSelected", 2);
        Debug.Log("Gyroo");
    }

    public void OpenSetingsMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void MenuSwitcher(GameObject menu)
    {
        if (menu.activeInHierarchy)
            menu.SetActive(false);
        else
            menu.SetActive(true);
    }
}
