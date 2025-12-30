using GlobalClasses;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollactablesManager : MonoBehaviour
{
    [SerializeField] MovingObjectsConfig MovingObjectsSO;

    [Header("Collectable Pool Fields")]
    [SerializeField] Transform CollactableParent;
    [SerializeField] Vector3 CollectableOffset;
    [SerializeField] int addSpeedPower = 1;
    public ObjectPoolManager CollectableObjectPool;
    public float PoolChance; // 0 to 100

    [Header("Spawn Settings")]
    [SerializeField] Transform leftSpawnPoint;
    [SerializeField] Transform middleSpawnPoint;
    [SerializeField] Transform rightpawnPoint;
    
    [Tooltip("Distance between rows of items")]
    [SerializeField] float spawnInterval = 10f; 
    private float _distanceMoved;

    private void Update()
    {
        // 1. Move existing items
        MoveObject();

        // 2. Spawn new items based on "Virtual Distance"
        HandleSpawning();
    }

    private void HandleSpawning()
    {
        // Calculate how much "ground" we covered this frame
        float moveAmount = GetSpeed() * addSpeedPower * Time.deltaTime * WorldSpeed.SpeedMultiplier;
        
        // Add to our tracker
        _distanceMoved += Mathf.Abs(moveAmount); // Use Abs in case speed is negative

        // If we have covered enough ground, try to spawn
        if (_distanceMoved >= spawnInterval)
        {
            _distanceMoved = 0; // Reset tracker
            PoolCollectable();
        }
    }

    public void PoolCollectable()
    {
        // Chance check (e.g., 50% chance to spawn a coin here, 50% empty space)
        if(Random.Range(0, 100) > PoolChance) return; 
        
        GameObject pooledObject = CollectableObjectPool.GetObject();
        pooledObject.transform.position = RandomSpawnPoint();
        
        // CRITICAL: Make sure the object is active and reset parenting if needed
        pooledObject.transform.SetParent(CollactableParent); 
    }

    private Vector3 RandomSpawnPoint()
    {
        // BUG FIX: Random.Range(int, int) is EXCLUSIVE on the max.
        // (0, 2) only returns 0 or 1. You need (0, 3) to get 0, 1, or 2.
        int random = Random.Range(0, 3); 
        
        switch (random) 
        {
            case 0:
                return leftSpawnPoint.position + CollectableOffset;
            case 1:
                return middleSpawnPoint.position  + CollectableOffset;
            case 2:
                return rightpawnPoint.position  + CollectableOffset;
            default:
                return middleSpawnPoint.position  + CollectableOffset;
        }
    }

    private void MoveObject()
    {
        // Vector3.back is the global direction "Towards Camera" (0, 0, -1)
        Vector3 direction = Vector3.back; 

        foreach (Transform obj in CollactableParent)
        {
            if (obj.gameObject.activeSelf)
            {
                float currentSpeed = GetSpeed() * addSpeedPower * Time.deltaTime * WorldSpeed.SpeedMultiplier;
                
                // ADD "Space.World" HERE
                // Now it ignores the object's 180 rotation and just moves South
                obj.Translate(direction * currentSpeed, Space.World);
                
                // Recycle check...
                if (obj.position.z < -20f) 
                {
                    CollectableObjectPool.ReleaseObject(obj.gameObject);
                }
            }
        }
    }

    private float GetSpeed()
    {
        return MovingObjectsSO.CollectableSpeed;
    }
    
    private void OnValidate()
    {
        if (addSpeedPower < 1) addSpeedPower = 1;
    }
}