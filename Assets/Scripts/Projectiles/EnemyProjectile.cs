using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    //[SerializeField] int damage = 5;
    [SerializeField] MovingObjectsConfig speed;

    void Update()
    {
        this.transform.Translate(speed.EnemyProjectileSpeed * Time.deltaTime * Vector3.back);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Enemy TakeDamage();
        }
    }

}
