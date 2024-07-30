using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobwebBullet : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        this.transform.Translate(speed * Time.deltaTime * Vector3.up);
    }

}
