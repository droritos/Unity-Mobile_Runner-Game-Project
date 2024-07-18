using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    [Header("Floor Data")]
    [SerializeField] Transform StartPositionPoint;

    [Header("Object Pool Fields")]
    [SerializeField] ObjectPoolManager poolCoinScript;
    [SerializeField] Vector3 obstacleOffset;
    [SerializeField] int coinChance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            //Debug.Log($"{other.gameObject.name} Triggered the wall");
            other.transform.position = StartPositionPoint.position;

            RemoveObstacleOnFloor(other.transform);


            if (RandomObstacleChance(coinChance))
            {
                GameObject pooledObject = poolCoinScript.GetObject();
                PlaceObstacleOnFloor(pooledObject, other.transform);
            }
        }
    }

    private void PlaceObstacleOnFloor(GameObject poolObject, Transform floor)
    {
        // Get the GroundController component from the parent of the floor
        GroundController current = floor.GetComponentInParent<GroundController>();

        // Calculate the x, y, z coordinates for placing the obstacle
        float x = RandomPosition(current.FloorBounds.size.x);

        // Create a Vector3 for the new position
        Vector3 position = new Vector3(x, obstacleOffset.y, 0);

        // Set the position of the poolObject to the calculated position
        poolObject.transform.position = position;

        // Set the parent of the poolObject to the floor, keeping its local position
        poolObject.transform.SetParent(floor, false);
    }

    private void RemoveObstacleOnFloor(Transform floor)
    {
        for (int i = 0; i < floor.childCount; i++)
        {
            Transform child = floor.GetChild(i);
            if (child.childCount > 0)
            {
                GameObject obstacle = child.GetChild(0).gameObject;
                poolCoinScript.ReleaseObject(obstacle);
                Debug.Log($"Released obstacle: {obstacle.name}");
            }
        }
    }
    private bool RandomObstacleChance(int odd)
    {
        int chance = Random.Range(0, 100);
        return chance <= odd; // % chance to place an obstacle
    }
    private float RandomPosition(float axisPosition)
    {
        List<float> floats = new List<float>();
        axisPosition = Mathf.Floor(axisPosition / 3); // Getting an int number
        float axisPositionLeft = axisPosition - obstacleOffset.x; // Left trail path of obstacles
        floats.Add(axisPositionLeft);

        float axisPositionMiddle = 0f; // Middle trail path of obstacles
        floats.Add(axisPositionMiddle);

        float axisPositionRight = (axisPosition - (axisPosition * 2)) + obstacleOffset.x; // Right trail path of obstacles
        floats.Add(axisPositionRight);

        int index = Random.Range(0, floats.Count);
        return floats[index];
    }
}
