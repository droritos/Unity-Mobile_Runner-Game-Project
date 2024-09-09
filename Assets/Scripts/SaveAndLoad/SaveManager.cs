using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoSingleton<SaveManager>
{
    [Header("File Storage Config")]
    [SerializeField] string fileName;

    [SerializeField] PlayerStatsConfig playerStatsConfig;

    private GameData _gameData;
    private List<ISavabale> _savingObjects;
    private FileDataHandler _fileDataHandler;
    void Start()
    {
        this._fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this._savingObjects = FindAllSavingObjects();
        LoadGame();
    }

    private void NewGame()
    {
        this._gameData = new GameData();
    }

    private void SaveGame()
    {
        foreach (ISavabale savingObject in _savingObjects)
        {
            savingObject.Save(ref _gameData);
        }

        _fileDataHandler.Save(_gameData);
    }

    private void LoadGame()
    {
        this._gameData = _fileDataHandler.Load();

        if (this._gameData == null)
        {
            NewGame();
            Debug.Log("No Data Found, New Game");
        }
        else
            Debug.Log("Loaded");

        foreach (ISavabale savingObject in _savingObjects)
        {
            savingObject.Load(_gameData);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISavabale> FindAllSavingObjects()
    {
        IEnumerable<ISavabale> savabales = FindObjectsOfType<MonoBehaviour>().OfType<ISavabale>();
        return new List<ISavabale>(savabales);
    }
}
