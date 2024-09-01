using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [Header("Ground Start Points")]
    [SerializeField] Transform FloorStartPos;
    [SerializeField] Transform SidewalkRightStartPos;
    [SerializeField] Transform SidewalkLeftStartPos;
    [SerializeField] Transform StreetFloorParent;

    [Header("Collectable Pool Fields")]
    [SerializeField] CollactablesManager coinContainer;
    [SerializeField] CollactablesManager lvlUpContainer;

    [Header("Building Pool Fields")]
    public BuildingObjectPool BuildingObjectPool;
    public Vector3 BuildingRightOffset;
    public Vector3 BuildingLeftOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            if (RandomObstacleChance(lvlUpContainer.PoolChance))
            {
                // Take LevelUp Object 
                other.transform.position = FloorStartPos.position; // Other is the Floor
                lvlUpContainer.CollectablePooled();
            }
            else
            {
                // Take Coin Object
                other.transform.position = FloorStartPos.position; // Other is the Floor
                coinContainer.CollectablePooled();
            }
        }
        else if (other.CompareTag("Coin"))
        {
            coinContainer.CollectableObjectPool.ReleaseObject(other.gameObject);
        }
        else if (other.CompareTag("LvLUp"))
        {
            lvlUpContainer.CollectableObjectPool.ReleaseObject(other.gameObject);
        }
        else if (other.CompareTag("SidewalkRight"))
        {
            other.transform.position = SidewalkRightStartPos.position;
            RemoveBuildingFromSidewalk(other.transform);
            PlaceBuildingOnSidewalk(other, -180, BuildingRightOffset);
        }
        else if (other.CompareTag("SidewalkLeft"))
        {
            other.transform.position = SidewalkLeftStartPos.position;
            RemoveBuildingFromSidewalk(other.transform);
            PlaceBuildingOnSidewalk(other, 0, BuildingLeftOffset);
        }
        else if (other.CompareTag("StreetFloor"))
        {
            other.transform.position = StreetFloorParent.position;
        }
    }

    #region << Pool Related Methods >> 
    private bool RandomObstacleChance(float odd)
    {
        int chance = Random.Range(0, 100);
        return chance <= odd; // % chance to place an obstacle
    }
    #endregion

    #region << Building Related Methods >>
    private void PlaceBuildingOnSidewalk(Collider other, int degrees , Vector3 offset)
    {
        GameObject build = RandomBuilding(degrees);
        GameObject pooledBuilding = BuildingObjectPool.GetObject(build);
        pooledBuilding.transform.SetParent(other.transform, true);
        pooledBuilding.transform.localPosition = offset;
    }
    private void RemoveBuildingFromSidewalk(Transform sidewalk)
    {
        for (int i = 0; i < sidewalk.childCount; i++)
        {
            GameObject building = sidewalk.GetChild(i).gameObject;
            BuildingObjectPool.ReleaseObject(building);
            //Debug.Log($"Released obstacle: {building.name}");
        }
    }
    public GameObject RandomBuilding(float rotationDegrees)
    {
        int ranom = Random.Range(0, BuildingObjectPool.Prefabs.Length);
        GameObject building = BuildingObjectPool.Prefabs[ranom];
        building.transform.rotation = Quaternion.Euler(0, rotationDegrees, 0);
        return building;
    }
    #endregion
}
