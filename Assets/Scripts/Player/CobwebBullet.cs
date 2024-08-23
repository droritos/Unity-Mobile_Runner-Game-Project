using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobwebBullet : MonoBehaviour
{
    [SerializeField] int damage = 5;
    [SerializeField] MovingObjectsConfig speed;

    void Update()
    {
        this.transform.Translate(speed.CobwebSpeed * Time.deltaTime * Vector3.forward);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Enemy TakeDamage();
        }
    }
}
