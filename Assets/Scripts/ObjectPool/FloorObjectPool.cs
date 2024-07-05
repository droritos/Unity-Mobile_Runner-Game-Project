using System.Collections.Generic;
using UnityEngine;

public class FloorObjectPool : MonoSingleton<FloorObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string type;
        public GameObject prefab;
        public int maxAmout;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    GameObject objectToSpawn;

    private void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.maxAmout; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.type, objectPool);
        }
    }

    public GameObject SpawnFromPool(string type, Vector3 position, Quaternion rotation)
    {
        if (!PoolDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"Pool with type {type} , Does not exits");
            return null;
        }

        objectToSpawn = PoolDictionary[type].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        PoolDictionary[type].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
