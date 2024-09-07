using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject Prefab;
    public int InitialPoolSize = 50;
    public int MaxPoolSize = 100;
    public ObjectPool<GameObject> Pool;
    [SerializeField] Transform parent;
    private Vector3 _localScaled;

    public Queue<GameObject> ActiveObjects = new Queue<GameObject>();

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
        _localScaled = new Vector3(0.5f, 0.5f, 0.5f);
        var gameobjectprefab = Instantiate(Prefab); // Spawn 
        gameobjectprefab.transform.SetParent(parent);
        gameobjectprefab.transform.position = parent.transform.position;
        return gameobjectprefab;
    }

    private void OnTakeFromPool(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogWarning($"Object not found : {obj.name}");
        }
        obj.SetActive(true);

    }

    public void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(parent);
    }

    public GameObject GetObject()
    {
        return Pool.Get();
    }

    public void ReleaseObject(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogWarning($"Object not found : {obj.name}");
        }
        Pool.Release(obj);
        
    }
    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    public void IsMaxPoolSize(Vector3 resetPosition)
    {

        if (ActiveObjects.Count > MaxPoolSize)
        {
            GameObject oldestActiveObj = ActiveObjects.Dequeue();  // Get the oldest active object
            oldestActiveObj.transform.position = resetPosition;
            Pool.Release(oldestActiveObj);
        }
    }
}