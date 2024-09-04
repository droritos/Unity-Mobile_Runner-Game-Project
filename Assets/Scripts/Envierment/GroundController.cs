using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [Header("Ground Config")]
    [SerializeField] MovingObjectsConfig floorConfig;
    private Dictionary<string, Bounds> objectBounds = new Dictionary<string, Bounds>();

    [Header("Ground Type Lists")]
    [SerializeField] List<Transform> groundPieces; // List of ground pieces
    [SerializeField] List<Transform> sidewalkRightPieces; // List of right sidewalk pieces
    [SerializeField] List<Transform> sidewalkLeftPieces; // List of left sidewalk pieces
    [SerializeField] List<Transform> StreetFloorPieces; // List of left sidewalk pieces


    [Header("Ground Type Parents")]
    [SerializeField] Transform floorParent;
    [SerializeField] Transform sidewalkRightParent;
    [SerializeField] Transform sidewalkLeftParent;
    [SerializeField] Transform StreetFloorParent;

    [Header("Referances")]
    [SerializeField] WallManager wallManager;



    private void Awake()
    {
        //SetAllObjects(floorParent, groundPieces, "Floor");
        //SetAllObjects(sidewalkRightParent, sidewalkRightPieces, "SidewalkRight");
        //SetAllObjects(sidewalkLeftParent, sidewalkLeftPieces, "SidewalkLeft");
        SetSideWalkPieces(sidewalkRightParent, sidewalkRightPieces);
        SetSideWalkPieces(sidewalkLeftParent, sidewalkLeftPieces);
        SetGroundPieces();
    }

    private void Start()
    {
        InsiliateStartingBuilding();
    }

    private void Update()
    {
        MoveObjects(groundPieces, floorConfig.CollectableSpeed);
        MoveObjects(sidewalkRightPieces, floorConfig.SideWalkSpeed);
        MoveObjects(sidewalkLeftPieces, floorConfig.SideWalkSpeed);
        MoveChildObjects(StreetFloorParent, floorConfig.SideWalkSpeed);
    }

    private void MoveObjects(List<Transform> objects, float speed)
    {
        foreach (Transform obj in objects)
        {
            // Move the object backwards along the Z axis
            obj.Translate(speed * Time.deltaTime * Vector3.back);
        }
    }
    private void MoveChildObjects(Transform parentPiece, float speed)
    {
        //Transform child = parentPiece.GetComponentInChildren<Transform>();
        foreach (Transform child in parentPiece.GetComponentInChildren<Transform>())
        {
            // Move the object backwards along the Z axis
            child.Translate(speed * Time.deltaTime * Vector3.back);
        }
    }


    private void SetAllObjects(Transform parent, List<Transform> objectList, string objectType)
    {
        Transform lastObject = PopulateTransformList(parent, objectList);
        UpdateBounds(lastObject, objectType);
    }

    private void SetGroundPieces()
    {
        foreach (Transform obj in floorParent)
        {
            groundPieces.Add(obj);
        }
    }
    private void SetSideWalkPieces(Transform parent, List<Transform> objectList)
    {
        foreach (Transform obj in parent)
        {
            objectList.Add(obj);
        }
    }



    private Transform PopulateTransformList(Transform parent, List<Transform> objectList)
    {
        objectList.Clear(); // Clear the list to avoid duplications
        Transform lastAddedTransform = null;

        for (int i = 0; i < parent.childCount; i++)
        {
            lastAddedTransform = parent.GetChild(i);
            objectList.Add(lastAddedTransform);
        }

        return lastAddedTransform;
    }

    private void UpdateBounds(Transform targetTransform, string objectType)
    {
        if (targetTransform != null)
        {
            Bounds bounds = targetTransform.GetComponent<MeshRenderer>().bounds;
            if (objectBounds.ContainsKey(objectType))
            {
                objectBounds[objectType] = bounds;
            }
            else
            {
                objectBounds.Add(objectType, bounds);
            }
        }
    }
    public Bounds GetBounds(string objectType)
    {
        if (objectBounds.ContainsKey(objectType))
        {
            return objectBounds[objectType];
        }
        else
        {
            return new Bounds();
        }
    }

    private void InsiliateStartingBuilding()
    {
        foreach (Transform sidewalk in sidewalkLeftParent) // Left
        {
            GameObject build = wallManager.RandomBuilding(0);
            GameObject pooledBuilding = wallManager.BuildingObjectPool.GetObject(build);
            pooledBuilding.transform.SetParent(sidewalk);
            pooledBuilding.transform.localPosition = wallManager.BuildingLeftOffset;
        }
        foreach (Transform sidewalk in sidewalkRightParent) // Right
        {
            GameObject build = wallManager.RandomBuilding(-180);
            GameObject pooledBuilding = wallManager.BuildingObjectPool.GetObject(build);
            pooledBuilding.transform.SetParent(sidewalk);
            pooledBuilding.transform.localPosition = wallManager.BuildingRightOffset;
        }

    }
}
