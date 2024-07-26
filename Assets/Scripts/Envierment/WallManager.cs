using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [Header("Ground Start Points")]
    [SerializeField] Transform FloorStartPos;
    [SerializeField] Transform SidewalkRightStartPos;
    [SerializeField] Transform SidewalkLeftStartPos;

    [Header("Coin Pool Fields")]
    [SerializeField] ObjectPoolManager poolCoinScript;
    [SerializeField] Vector3 coinOffset;
    [SerializeField] int coinChance;

    [Header("Building Pool Fields")]
    [SerializeField] BuildingObjectPool buildingObjectPool;
    [SerializeField] Vector3 buildingRightOffset;
    [SerializeField] Vector3 buildingLeftOffset;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            other.transform.position = FloorStartPos.position; // Other is the Floor

            RemoveObstacleOnFloor(other.transform);


            if (RandomObstacleChance(coinChance))
            {
                GameObject pooledObject = poolCoinScript.GetObject();
                Debug.Log($"Active Coin");
                PlaceObstacleOnFloor(pooledObject, other.transform, "Floor");
            }
        }
        else if (other.CompareTag("SidewalkRight"))
        {
            other.transform.position = SidewalkRightStartPos.position;
            RemoveBuildingFromSidewalk(other.transform);
            PlaceBuildingOnSidewalk(other, -180 , buildingRightOffset);
        }
        else if (other.CompareTag("SidewalkLeft"))
        {
            other.transform.position = SidewalkLeftStartPos.position;
            RemoveBuildingFromSidewalk(other.transform);
            PlaceBuildingOnSidewalk(other, 0, buildingLeftOffset);
        }
    }

    private void PlaceBuildingOnSidewalk(Collider other, int degrees , Vector3 offset)
    {
        GameObject build = RandomBuilding(degrees);
        GameObject pooledBuilding = buildingObjectPool.GetObject(build);
        pooledBuilding.transform.SetParent(other.transform, true);
        pooledBuilding.transform.localPosition = offset;
    }

    #region << Coin Related Methods >> 
    private void PlaceObstacleOnFloor(GameObject poolObject, Transform floor, string objectType)
    {
        // Get the GroundController component from the parent of the floor
        GroundController current = floor.GetComponentInParent<GroundController>();

        // Retrieve the bounds for the specified object type
        Bounds bounds = current.GetBounds(objectType);

        // Calculate the x, y, z coordinates for placing the obstacle
        float x = RandomPosition(bounds.size.x);

        // Create a Vector3 for the new position
        Vector3 position = new Vector3(x, coinOffset.y, 0);

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
        float axisPositionLeft = axisPosition - coinOffset.x; // Left trail path of obstacles
        floats.Add(axisPositionLeft);

        float axisPositionMiddle = 0f; // Middle trail path of obstacles
        floats.Add(axisPositionMiddle);

        float axisPositionRight = (axisPosition - (axisPosition * 2)) + coinOffset.x; // Right trail path of obstacles
        floats.Add(axisPositionRight);

        int index = Random.Range(0, floats.Count);
        return floats[index];
    }
    #endregion
    #region << Coin Related Methods >>
    private GameObject RandomBuilding(int degres)
    {
        int ranom = Random.Range(0, buildingObjectPool.Prefabs.Length);
        GameObject building = buildingObjectPool.Prefabs[ranom];
        building.transform.rotation = Quaternion.Euler(0, degres, 0);
        return building;
    }
    private void RemoveBuildingFromSidewalk(Transform sidewalk)
    {
        for (int i = 0; i < sidewalk.childCount; i++)
        {
            Transform child = sidewalk.GetChild(i);
            if (child.childCount > 0)
            {
                GameObject building = child.GetChild(0).gameObject;
                buildingObjectPool.ReleaseObject(building, building);
                Debug.Log($"Released obstacle: {building.name}");
            }
        }
    }

    private void RotateBuilding()
    {

    }
    #endregion
}
