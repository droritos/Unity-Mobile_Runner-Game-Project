using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IsScriptsExists : MonoBehaviour
{
    [SerializeField] GameObject sceneStateManagerPrefab; 

    [SerializeField] GameObject gameManager;

    void Start()
    {
        IsGameManagerExtis();
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

}
