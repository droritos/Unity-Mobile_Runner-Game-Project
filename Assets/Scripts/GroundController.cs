using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [Header("Ground Floor Data")]
    [SerializeField] RoadParameters floorConfig;
    [SerializeField] List<Transform> groundPieces; // Array of ground pieces
    private float groundLength;

    [Header("Object Pool")]
    [SerializeField] ObjectPoolManager obstaclePool;

    private void Awake()
    {
        GetAllFloors();
    }
    void Update()
    {
        MoveFloor();
    }

    private void MoveFloor()
    {
        foreach (Transform floor in groundPieces)
        {
            // Move the ground backwards along the Z axis
            floor.Translate(floorConfig.Speed * Time.deltaTime * Vector3.back);
        }
    }

    private void GetAllFloors()
    {
        groundPieces = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            groundPieces.Add(transform.GetChild(i));
        }
    }
}



/*
 
public class MoveGround : MonoBehaviour
{
    private float _speed;
    Transform spawnFloorPosition;
    private void Awake()
    {
        spawnFloorPosition = GameObject.Find("SpawnFloors").transform;
    }
    void Update()
    {
        MoveFloor();
        UpdateMoveSpeed();
    }

    private void MoveFloor()
    {
        // Move the ground backwards along the Z axis
        transform.Translate(_speed * Time.deltaTime * Vector3.back);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            transform.position = spawnFloorPosition.transform.position;
        }
    }
    private void UpdateMoveSpeed()
    {
        _speed = GameManager.Instance.SetFloorSpeed();
    }
}

*/