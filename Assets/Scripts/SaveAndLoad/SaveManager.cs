using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoSingleton<SaveManager>
{
    [Header("File Storage Config")]
    [SerializeField] string fileName;

    private GameData _gameData;
    private List<ISavabale> _savingObjects;
    private FileDataHandler _fileDataHandler;
    void Start()
    {
        this._fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this._savingObjects = FindAllSavingObjects();
        LoadGame();
    }
    public void CallSaveGameMethod() // Need to be called when player level up
    {
        SaveGame();
    }

    public void DeleteFileSavedFile() // Need to happen after player died so it wont save after he died
    {
        _fileDataHandler.DeleteSaveFile();
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

        //Debug.Log("Saving");
    }

    private void LoadGame()
    {
        this._gameData = _fileDataHandler.Load();

        if (this._gameData == null) // Starts new game data without setting all the data to 0 in to the player stats
        {
            NewGame();
            Debug.Log("No Data Found, Applying New Game");
        }
        else // Found the file than intilize the saved data to the object
        {
            foreach (ISavabale savingObject in _savingObjects)
            {
                savingObject.Load(_gameData);
            }
            Debug.Log("Loaded");
        }
    }
    private List<ISavabale> FindAllSavingObjects()
    {
        IEnumerable<ISavabale> savabales = FindObjectsOfType<MonoBehaviour>().OfType<ISavabale>();
        return new List<ISavabale>(savabales);
    }

    //    /*
    //private void OnApplicationQuit()
    //{

    //    SaveGame();
    //}
    //*/ // OnApplicationQuit

}
