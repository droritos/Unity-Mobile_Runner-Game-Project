using System;
using System.Collections;
using System.Collections.Generic;
using GlobalClasses;
using Interfaces;
using Manager;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour , IPausable
{
    [Header("Public Data")]
    public ObjectPoolManager ProjectilePoolScript;
    [SerializeField] Transform[] projectileSpawnPoint;

    [Header("Private Data")]
    [SerializeField] PlayerVisuals visualsController;
    private float _fire = 0;
    private PlayerStatsConfig _playerStatsConfig;
    
    private bool _isPaused;
    private bool _canShoot;
    private const string LaneTag = "Lane"; 
    
    void Start()
    {
        PauseManager.Instance?.Register(this);

        this._playerStatsConfig = GameManager.Instance.Player.PlayerStatsConfig;
        //Debug.Log($"Your fire rate is {_playerStatsConfig.FireCooldown} By global {_playerStatsConfig.G_FireCooldown}");
    }

    void OnDestroy()
    {
        PauseManager.Instance?.Unregister(this);
    }

    #region << Triggers >>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(LaneTag)) _canShoot = true;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(LaneTag)) _canShoot = false;
    }
    #endregion

    // void Update()
    // {
    //     if(_isPaused || !_canShoot) return;
    //     _fire += Time.deltaTime;
    //     AutoShoot();
    // }
    public void Shoot()
    {
        GetProjectileToShoot();
        //visualsController.Shoot();
        //StartCoroutine(WaitForShoot());
    }
    private void AutoShoot()
    {
        if (_fire >= _playerStatsConfig.FireCooldown)
        {
            Shoot();
            _fire = 0;
        }
    }

    private IEnumerator WaitForShoot()
    {
        if (visualsController == null)
        {
            Debug.LogError("WaitForShoot: visualsController is NULL", this);
            yield break;
        }

        float animLen = visualsController.GetCurrentAnimationLength();
        yield return new WaitForSeconds(animLen);
        
        AudioManager.Instance.PlaySFXByType(RandomAudioType.PlayerLaser);

        if (ProjectilePoolScript == null)
        {
            Debug.LogError("WaitForShoot: ProjectilePoolScript is NULL", this);
            yield break;
        }

        GetProjectileToShoot();
        // optionally:
        // web.transform.rotation = projectileSpawnPoint[0].rotation;
        // web.SetActive(true);
    }

    private void GetProjectileToShoot()
    {
        GameObject projectile = ProjectilePoolScript.GetObject();
        if (projectile == null)
        {
            Debug.LogError("WaitForShoot: Pool returned NULL object", this);
            return;
        }

        if (projectileSpawnPoint == null || projectileSpawnPoint.Length == 0 || projectileSpawnPoint[0] == null)
        {
            Debug.LogError("WaitForShoot: projectileSpawnPoint[0] is NULL / not set", this);
            return;
        }

        projectile.transform.position = projectileSpawnPoint[0].position;
    }

    public void SetPaused(bool paused)
    {
        _isPaused = paused;
    }
}
