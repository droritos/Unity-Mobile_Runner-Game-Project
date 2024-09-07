using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IsScriptsExists : MonoBehaviour
{
    [SerializeField] GameObject sceneStateManagerPrefab; 
    private SceneStateManager _sceneStateManager;

    [SerializeField] GameObject gameManager;

    void Start()
    {
        IsGameManagerExtis();
    }

    private void HandleSceneStateManager()
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
    }

    private void IsGameManagerExtis()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();
        if (gm == null)
        {
            GameObject newGameManager = Instantiate(gameManager);
            gm = newGameManager.GetComponent<GameManager>();
            DontDestroyOnLoad (newGameManager);
            Debug.Log("GameManager created and set to DontDestroyOnLoad.");
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

}
