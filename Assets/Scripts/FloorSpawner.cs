using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoSingleton<FloorSpawner>
{
    [SerializeField] float groundSpawnDistance = 50f;
    private bool spawningFloor = false;

    public void SpawnGround()
    {
        FloorObjectPool.Instance.SpawnFromPool("ground" , new Vector3(0,0, groundSpawnDistance), Quaternion.identity);
    }
}
