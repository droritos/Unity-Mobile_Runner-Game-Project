using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] float floorSpeed;
    [SerializeField] RoadParameters roadParameters;
    [SerializeField] ObjectPoolManager poolManager;
    List<GameObject> pooledList = new List<GameObject>();
    List<GameObject> standByList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolManager.InitialPoolSize; i++)
        {
            poolManager.GetObject();
            pooledList.Add(poolManager.GetObject());
        }
    }
    public float SetFloorSpeed()
    {
        roadParameters.Speed = floorSpeed;
        return floorSpeed;
    }
}
