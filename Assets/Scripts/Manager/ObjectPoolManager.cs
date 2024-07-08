using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{

// create bone section so voxels can be connected to it

    public GameObject Prefab;
    public int InitialPoolSize = 50;
    public int MaxPoolSize = 100;
    public ObjectPool<GameObject> Pool;
    [SerializeField] Transform poolParent;
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
            OnReturnedToPool(obj);
        }
    }

    private GameObject CreatePooledItem()
    {
        var gameobjectprefab = Instantiate(Prefab); // spwan 
        return gameobjectprefab;
    }

    private void OnTakeFromPool(GameObject obj)
    {
        obj.transform.SetParent(poolParent);
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