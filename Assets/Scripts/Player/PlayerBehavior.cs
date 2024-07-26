using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBehavior : MonoBehaviour
{
    public int coins = 0;

    [SerializeField] Transform playerScreenPhoto;
    [SerializeField] ObjectPoolManager coinPool;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinPool.ReleaseObject(other.gameObject);
            coins++;
            //Destroy(other.gameObject);
        }
    }
}
