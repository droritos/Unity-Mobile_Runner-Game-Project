using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobwebBullet : MonoBehaviour
{
    [SerializeField] MovingObjectsConfig speed;
    [SerializeField] ObjectPoolManager cobweb;
    [SerializeField] float lifeTime = 3f;

    void Update()
    {
        this.transform.Translate(speed.CobwebSpeed * Time.deltaTime * Vector3.forward);
        
    }
}
