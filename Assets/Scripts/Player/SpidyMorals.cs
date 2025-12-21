using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpidyMorals : MonoBehaviour
{
    [Header("Public Data")]
    [SerializeField] Transform[] CobwebSpawnPoint;
    public ObjectPoolManager CobwebPoolScript;

    [Header("Private Data")]
    [SerializeField] PlayerVisuals visualsController;
    private float _fire = 0;
    private PlayerStatsConfig _playerStatsConfig;
    
    void Start()
    {
        this._playerStatsConfig = GameManager.Instance.Player.PlayerStatsConfig;
        //_animator = GameManager.Instance.PlayerManager.MyAnimator;
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
        // Wait until animation is done
        yield return new WaitForSeconds(visualsController.GetCurrentAnimationLength());

        // Attempt to get an object from the pool
        GameObject web = CobwebPoolScript.GetObject();
        //cobwebPoolScript.ActiveObjects.Enqueue(web);
        web.transform.position = CobwebSpawnPoint[0].position;
    }
}
