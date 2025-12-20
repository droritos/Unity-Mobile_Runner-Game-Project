using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGarbage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            GameManager.Instance.BulletPool.ReleaseObject(other.gameObject);
            //Debug.Log("Enemy bullet realsed");
        }
        else if (other.CompareTag("Web"))
        {
            GameManager.Instance.PlayerManager.SpidyMorals.CobwebPoolScript.ReleaseObject(other.gameObject);
        }
    }
}
