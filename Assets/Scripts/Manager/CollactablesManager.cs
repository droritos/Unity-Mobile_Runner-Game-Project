using System.Collections.Generic;
using UnityEngine;

public class CollactablesManager : MonoBehaviour
{
    [SerializeField] MovingObjectsConfig MovingObjectsSO;

    [Header("Collactable Pool Fields")]
    [SerializeField] Transform CollactableParent;
    [SerializeField] Vector3 CollectableOffset;
    [SerializeField] int addSpeedPower = 0;
    public ObjectPoolManager CollectableObjectPool;
    public float PoolChance;

    [Header("SpawnPoints")]
    [SerializeField] Transform leftSpawnPoint;
    [SerializeField] Transform middleSpawnPoint;
    [SerializeField] Transform rightpawnPoint;

    private int _speed;


    private void Update()
    {
        _speed = (int)MovingObjectsSO.CollectableSpeed + addSpeedPower;
        MoveObject();
    }

    public void CollectablePooled()
    {
        GameObject pooledObject = CollectableObjectPool.GetObject();
        pooledObject.transform.position = RandomSpawnPoint() + CollectableOffset;
    }
    public void ReleaseMe(GameObject collatableObject)
    {
        CollectableObjectPool.ReleaseObject(collatableObject);
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

    private void MoveObject()
    {
        foreach (Transform obj in CollactableParent)
        {
            if (obj.gameObject.activeSelf)
            {
                obj.Translate(_speed * Time.deltaTime * Vector3.back);
            }
        }
    }

}
