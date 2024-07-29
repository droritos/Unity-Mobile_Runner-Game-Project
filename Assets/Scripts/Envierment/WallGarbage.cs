using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGarbage : MonoBehaviour
{
    [SerializeField] BuildingObjectPool buildingObjectPool;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Web"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Building"))
        {
            Debug.Log("Remove Building");
            buildingObjectPool.ReleaseObject(other.gameObject);
        }
    }

}
