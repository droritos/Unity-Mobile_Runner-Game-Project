using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [Header("Public Data")]
    public ObjectPoolManager ProjectilePoolScript;
    [SerializeField] Transform[] projectileSpawnPoint;

    [Header("Private Data")]
    [SerializeField] PlayerVisuals visualsController;
    private float _fire = 0;
    private PlayerStatsConfig _playerStatsConfig;
    
    void Start()
    {
        this._playerStatsConfig = GameManager.Instance.Player.PlayerStatsConfig;
        Debug.Log($"Your fire rate is {_playerStatsConfig.FireCooldown} By global {_playerStatsConfig.G_FireCooldown}");
    }

    void Update()
    {
        _fire += Time.deltaTime;
        AutoShoot();
    }
    private void AutoShoot()
    {
        if (_fire >= _playerStatsConfig.FireCooldown)
        {
            visualsController.Shoot();
            StartCoroutine(WaitForShoot());
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

        if (ProjectilePoolScript == null)
        {
            Debug.LogError("WaitForShoot: ProjectilePoolScript is NULL", this);
            yield break;
        }

        GameObject web = ProjectilePoolScript.GetObject();
        if (web == null)
        {
            Debug.LogError("WaitForShoot: Pool returned NULL object", this);
            yield break;
        }

        if (projectileSpawnPoint == null || projectileSpawnPoint.Length == 0 || projectileSpawnPoint[0] == null)
        {
            Debug.LogError("WaitForShoot: projectileSpawnPoint[0] is NULL / not set", this);
            yield break;
        }

        web.transform.position = projectileSpawnPoint[0].position;
        // optionally:
        // web.transform.rotation = projectileSpawnPoint[0].rotation;
        // web.SetActive(true);
    }

}
