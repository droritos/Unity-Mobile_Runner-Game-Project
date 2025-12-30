using System;
using System.Collections.Generic;
using GlobalClasses;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollactablesManager : MonoBehaviour
{
    [SerializeField] MovingObjectsConfig MovingObjectsSO;

    [Header("Collactable Pool Fields")]
    [SerializeField] Transform CollactableParent;
    [SerializeField] Vector3 CollectableOffset;
    [SerializeField] int addSpeedPower = 1;
    public ObjectPoolManager CollectableObjectPool;
    public float PoolChance;

    [Header("SpawnPoints")]
    [SerializeField] Transform leftSpawnPoint;
    [SerializeField] Transform middleSpawnPoint;
    [SerializeField] Transform rightpawnPoint;

    private void Update()
    {
        MoveObject();
    }

    private void OnValidate()
    {
        if (addSpeedPower < 1)
            addSpeedPower = 1;
    }
    public void CollectablePooled()
    {
        GameObject pooledObject = CollectableObjectPool.GetObject();
        pooledObject.transform.position = RandomSpawnPoint();
    }

    private Vector3 RandomSpawnPoint()
    {
        int random = Random.Range(0, 2);
        switch (random) 
        {
            case 0:
                return leftSpawnPoint.position + CollectableOffset;
            case 1:
                return middleSpawnPoint.position  + CollectableOffset;
            case 2:
                return rightpawnPoint.position  + CollectableOffset;
            default:
                return middleSpawnPoint.position  + CollectableOffset;;
        }
    }

    private void MoveObject()
    {
        foreach (Transform obj in CollactableParent)
        {
            if (obj.gameObject.activeSelf)
            {
                obj.Translate(GetSpeed() * addSpeedPower * Time.deltaTime * Vector3.forward);
            }
        }
    }

    private float GetSpeed()
    {
        switch (CollectableObjectPool.ObjectPoolType)
        {
            case ObjectPoolType.Coin:
                return MovingObjectsSO.CollectableSpeed;

            case ObjectPoolType.LevelUp:
                return -MovingObjectsSO.CollectableSpeed;
            default:
                return MovingObjectsSO.CollectableSpeed;
        }
    }

}
