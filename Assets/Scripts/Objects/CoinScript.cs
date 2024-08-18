using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] RoadParameters roadParametersSO;

    [Header("Coin Pool Fields")]
    [SerializeField] ObjectPoolManager poolCoinScript;
    [SerializeField] int coinChance;
    [SerializeField] Transform coinParent;
    public List<Transform> coinPieces;

    [Header("SpawnPoints")]
    [SerializeField] Transform leftSpawnPoint;
    [SerializeField] Transform middleSpawnPoint;
    [SerializeField] Transform rightpawnPoint;

    void Start()
    {
        GetCoins();
    }
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

    private void GetCoins()
    {
        foreach (Transform obj in coinParent)
        {
            coinPieces.Add(obj);
        }
    }
    private void MoveCoins()
    {
        for (int i = 0; i < coinPieces.Count; i++)
        {
            if (coinPieces[i].gameObject.activeSelf)
            {
                coinPieces[i].Translate(roadParametersSO.CarRoadSpeed * Time.deltaTime * Vector3.back);
            }
        }
    }

}
