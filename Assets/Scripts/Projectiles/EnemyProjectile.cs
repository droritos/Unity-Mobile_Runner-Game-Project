using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour , IPausable
{
    //[SerializeField] int damage = 4;
    [SerializeField] MovingObjectsConfig speed;
    
    private bool _isPaused;

    private void Start()
    {
        PauseManager.Instance?.Register(this);
    }

    private void OnDestroy()
    {
        PauseManager.Instance?.Unregister(this);
    }
    void Update()
    {
        if(_isPaused) return;
        this.transform.Translate(speed.EnemyProjectileSpeed * Time.deltaTime * Vector3.back);
    }

    public void SetPaused(bool paused)
    {
        _isPaused = paused;
    }
}
