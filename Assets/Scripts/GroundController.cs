using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [Header("Ground Floor Data")]
    [SerializeField] RoadParameters floorConfig;
    [SerializeField] Transform[] groundPieces; // Array of ground pieces
    private float groundLength;

    [Header("Object Pool")]
    [SerializeField] ObjectPoolManager obstaclePool;


    private void Start()
    {
        if (groundPieces.Length > 0)
        {
            groundLength = groundPieces[0].GetComponent<Renderer>().bounds.size.z;
        }
    }

    private void Update()
    {
        MoveFloor();
        CheckAndRepositionGround();
    }

    private void MoveFloor()
    {
        foreach (Transform groundPiece in groundPieces)
        {
            groundPiece.position += floorConfig.Speed * Time.deltaTime * Vector3.back;

        }

    }

    private void CheckAndRepositionGround()
    {
        foreach (Transform groundPiece in groundPieces)
        {

            if (groundPiece.position.z < -groundLength)
            {

                Transform lastGroundPiece = GetLastGroundPiece();
                float newZ = lastGroundPiece.position.z + groundLength;
                groundPiece.position = new Vector3(groundPiece.position.x, groundPiece.position.y, newZ);

                GameObject obstacle = obstaclePool.GetObject();
                obstacle.transform.SetParent(groundPiece);
            }
        }
    }

    private Transform GetLastGroundPiece()
    {
        Transform lastGroundPiece = groundPieces[0];
        foreach (Transform groundPiece in groundPieces)
        {
            if (groundPiece.position.z > lastGroundPiece.position.z)
            {
                lastGroundPiece = groundPiece;
            }
        }
        return lastGroundPiece;
    }


    private void CreatPoolObject()
    {
        for (int i = 0; i > obstaclePool.InitialPoolSize; i++)
        {
            obstaclePool.GetObject();
            
        }
    }
}