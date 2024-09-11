using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Reading Saved Data")]
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
        //readListFromFile.LoadPlayerData();
        //StartCoroutine(webcamCapture.RequestCameraPermissionCoroutine());
    }

}
