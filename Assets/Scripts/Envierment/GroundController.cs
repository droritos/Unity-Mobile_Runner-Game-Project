using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [Header("Ground Floor Data")]
    [SerializeField] RoadParameters floorConfig;
    [SerializeField] List<Transform> groundPieces; // Array of ground peices
    [HideInInspector] public Bounds FloorBounds;

    private void Awake()
    {
        SetAllFloors();
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
    private Transform GetAllFloors()
    {
        groundPieces = new List<Transform>();
        Transform lastAddedTransform = null;

        for (int i = 0; i < transform.childCount; i++)
        {
            lastAddedTransform = transform.GetChild(i);
            groundPieces.Add(lastAddedTransform);
        }

        // Return the last added Transform
        return lastAddedTransform;
    }
    private void GetFloorBounds(Transform floorBounds)
    {
        Bounds bounds = floorBounds.GetComponent<MeshFilter>().mesh.bounds;
        FloorBounds = bounds;
    }
    private void SetAllFloors()
    {
        Transform takeFloorBounds = GetAllFloors();
        GetFloorBounds(takeFloorBounds);
    }
    public bool RandomObstacleOnMe()
    {
        int chance = Random.Range(0, 100);
        if (chance > 50)
            return true; // Add an obstacle on the floor 
        else
            return false; // Dont add an obstacle on the floor
    }
}