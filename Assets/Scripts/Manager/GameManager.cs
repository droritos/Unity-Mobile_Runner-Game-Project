using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] float floorSpeed;
    [SerializeField] RoadParameters roadParameters;
    [SerializeField] ReadListFromFile readListFromFile;
    [SerializeField] WebcamCapture webcamCapture;
    [SerializeField] PlayerBehavior player;
    [SerializeField] SceneStateManager sceneStateManager;

    private int _checkPoint = 0;

    public float SetFloorSpeed()
    {
        roadParameters.CarRoadSpeed = floorSpeed;
        return floorSpeed;
    }

    private void Start()
    {
        readListFromFile.LoadPlayerData();
        StartCoroutine(webcamCapture.RequestCameraPermissionCoroutine());
    }

    private void Update()
    {
        AutoSave();
    }

    private void AutoSave()
    {
        if (player.coins % 5 == 0)
        {
            _checkPoint++;
            sceneStateManager.SaveSceneState($"checkPoint_{_checkPoint}");
        }
    }
}
