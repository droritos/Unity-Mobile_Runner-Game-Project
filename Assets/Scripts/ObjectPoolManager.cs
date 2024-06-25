using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int initialPoolSize;
    [SerializeField] int maxPoolSize;

    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            CreatedPooledItem,
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroyPoolObject,
            true,
            initialPoolSize,
            maxPoolSize
            );

        // Pre - Warm
        for (int i = 0; i < initialPoolSize; i++)
        {
            
        }
    }

    private GameObject CreatedPooledItem()
    {
        GameObject voxel = Instantiate(prefab);
        return voxel;
    }

    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void OnReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetObject()
    {
        return pool.Get();
    }

    public void ReleaseObject(GameObject obj)
    {
        pool.Release(obj);
    }
}
