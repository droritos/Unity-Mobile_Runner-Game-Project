using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] MovingObjectsConfig MovingObjectsSO;

    [Header("Coin Pool Fields")]
    public ObjectPoolManager poolCoinScript;
    [SerializeField] int coinChance;
    [SerializeField] Transform coinParent;

    [Header("SpawnPoints")]
    [SerializeField] Transform leftSpawnPoint;
    [SerializeField] Transform middleSpawnPoint;
    [SerializeField] Transform rightpawnPoint;
    private void Update()
    {
        MoveCoins();
    }

    public void CoinPooled()
    {
        GameObject pooledObject = poolCoinScript.GetObject();
        pooledObject.transform.position = RandomSpawnPoint();
    }
    public void ReleaseCoin(GameObject coin)
    {
        poolCoinScript.ReleaseObject(coin);
    }

    private Vector3 RandomSpawnPoint()
    {
        int random = Random.Range(0, 2);
        switch (random) 
        {
            case 0:
                return leftSpawnPoint.position;
            case 1:
                return middleSpawnPoint.position;
            case 2:
                return rightpawnPoint.position;
            default:
                return middleSpawnPoint.position;
        }
    }

    private void MoveCoins()
    {
        foreach (Transform obj in coinParent)
        {
            if (obj.gameObject.activeSelf)
            {
                obj.Translate(MovingObjectsSO.CarRoadSpeed * Time.deltaTime * Vector3.back);
            }
        }
    }

}
