using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] float floorSpeed;
    [SerializeField] RoadParameters roadParameters;
    [SerializeField] ReadListFromFile readListFromFile;
    [SerializeField] WebcamCapture webcamCapture;

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
}
