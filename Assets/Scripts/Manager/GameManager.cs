using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] float floorSpeed;
    [SerializeField] MovingObjectsConfig roadParameters;
    [SerializeField] ReadListFromFile readListFromFile;
    [SerializeField] WebcamCapture webcamCapture;

    [Header("Spidy")]
    public PlayerBehavior Player;
    public SpidyMorals SpidyMorals;
    public UpgradeMenu UpgradeMenuScript;

    [Header("Enemy")]
    public ObjectPoolManager EnemyPool;
    public ObjectPoolManager BulletPool;

    private void Start()
    {
        readListFromFile.LoadPlayerData();
        StartCoroutine(webcamCapture.RequestCameraPermissionCoroutine());
    }
}
