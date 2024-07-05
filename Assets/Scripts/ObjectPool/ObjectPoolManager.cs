using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{

// create bone section so voxels can be connected to it

    public GameObject Prefab;
    public int InitialPoolSize = 100;
    public int MaxPoolSize = 4500;
    public ObjectPool<GameObject> Pool;
    void Awake()
    {
        Pool = new ObjectPool<GameObject>(
            CreatePooledItem,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            true,
            InitialPoolSize,
            MaxPoolSize
        );

        // Pre-warm the pool
        for (int i = 0; i < InitialPoolSize; i++)
        {
            GameObject obj = Pool.Get();
            Pool.Release(obj);
        }
    }

    private GameObject CreatePooledItem()
    {
        var gameobjectprefab = Instantiate(Prefab); // spwan 
        return gameobjectprefab;
    }

    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
        // obstacles 
        // power ups
    }

    public void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);

    }

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetObject()
    {
        return Pool.Get();
    }

    public void ReleaseObject(GameObject obj)
    {
        Pool.Release(obj);
    }
}