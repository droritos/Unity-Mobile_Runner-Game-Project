using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BuildingObjectPool : MonoBehaviour
{
    public GameObject[] Prefabs; // Array of building prefabs
    public int InitialPoolSize = 50;
    public int MaxPoolSize = 100;
    private Dictionary<GameObject, ObjectPool<GameObject>> prefabPools; // Dictionary to store pools for each prefab
    private Vector3 _localScaled;


    void Awake()
    {
        prefabPools = new Dictionary<GameObject, ObjectPool<GameObject>>();

        foreach (var prefab in Prefabs)
        {
            var pool = new ObjectPool<GameObject>(
                () => CreatePooledItem(prefab),
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                true,
                InitialPoolSize,
                MaxPoolSize
            );

            prefabPools.Add(prefab, pool);
            _localScaled = prefab.transform.localScale;
            // Pre-warm the pool
            for (int i = 0; i < InitialPoolSize; i++)
            {
                GameObject obj = pool.Get();
                OnReturnedToPool(obj);
            }
        }
    }

    private GameObject CreatePooledItem(GameObject prefab)
    {
        var instance = Instantiate(prefab); // Instantiate prefab
        instance.transform.SetParent(this.transform, false); // Set parent if needed
        //instance.transform.parent = this.transform;
        var pooledObject = instance.AddComponent<PooledObject>();
        pooledObject.OriginalPrefab = prefab;
        return instance;
    }

    private void OnTakeFromPool(GameObject obj)
    {
        obj.transform.localScale = _localScaled;
        //obj.name = prefabPools.Keys.ToString();
        obj.SetActive(true);
        // Additional logic for obstacles, power-ups, etc.
    }

    public void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(this.transform);
        obj.transform.position = Vector3.zero; // Restet position
    }

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetObject(GameObject prefab)
    {
        if (prefabPools.ContainsKey(prefab))
        {
            return prefabPools[prefab].Get();
        }
        else
        {
            Debug.LogError("Prefab not found in pool manager.");
            return null;
        }
    }

    public void ReleaseObject(GameObject obj)
    {
        var pooledObject = obj.GetComponent<PooledObject>();
        if (pooledObject != null && prefabPools.ContainsKey(pooledObject.OriginalPrefab))
        {
            prefabPools[pooledObject.OriginalPrefab].Release(obj);
        }
        else
        {
            Debug.LogError("Prefab not found in pool manager.");
        }
    }
}
