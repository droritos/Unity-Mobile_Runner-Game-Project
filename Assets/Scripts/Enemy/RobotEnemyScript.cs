using UnityEngine;
using System.Collections;

public class RobotEnemyScript : MonoBehaviour
{
    [Header("Enemy Fields")]
    [SerializeField] int maxHealth = 5;
    [SerializeField] float fireColdown = 1f;
    [SerializeField] float hitDuration = 0.1f; // Duration to keep the object red
    [SerializeField] Transform projectileSpawnPoint;

    [Header("Enemy Pools")]

    [Header("Enemy Visual")]
    [SerializeField] Transform robotGFX;

    private float _fire = 0;
    private Animator _animator;
    private Renderer objectRenderer;
    private GameObject _bulletProjectile;
    private int currentHealth;

    private ObjectPoolManager bulletPool;
    private ObjectPoolManager enemyPool;


    void Start()
    {
        _animator = GetComponent<Animator>();

        // Find the Renderer in the child object "Robot1"
        objectRenderer = robotGFX.GetComponent<Renderer>();

    }

    void Update()
    {
        ShootProjectile();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Web"))
        {
            TakeDamage(GameManager.Instance.Player.CobwebDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        // Decrease health or dead
        currentHealth -= damage;
        StartCoroutine(HitAnimation());

        if (currentHealth <= 0)
        {
            Die();
            GameManager.Instance.UpgradeMenuScript.GainExperience(50);
        }
    }

    private IEnumerator HitAnimation()
    {
        if (objectRenderer != null)
        {
            // Change the color to red
            objectRenderer.material.color = Color.red;
            // Wait for the specified duration
            yield return new WaitForSeconds(hitDuration);
            // Revert back to the original color (white) 
            if (objectRenderer != null)
                objectRenderer.material.color = Color.white;

            //Debug.Log($"Ouch You Hit Me");
        }
        else
        {
            Debug.Log("Object Renderer Not Found");
        }
    }

    private void ShootProjectile()
    {
        _fire += Time.deltaTime;
        if (_fire >= fireColdown)
        {
            _animator.SetTrigger("Attack");
            StartCoroutine(WaitForShoot());
            _fire = 0;
        }
    }

    private void Die()
    {
        if (GameManager.Instance.EnemyPool != null)
        {
            GameManager.Instance.EnemyPool.ReleaseObject(this.gameObject);
            this.transform.position = GameManager.Instance.EnemyPool.transform.position;
            ResetEnemy();
        }
        else
        {
            Debug.LogWarning("Enemy Pool Not Found");
        }
    }

    private void ResetEnemy()
    {
        currentHealth = maxHealth;
        objectRenderer.material.color = Color.white;
    }

    private IEnumerator WaitForShoot()
    {
        // Wait until animation is done
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        // Attempt to get an object from the pool
        _bulletProjectile = GameManager.Instance.BulletPool.GetObject();
        _bulletProjectile.transform.position = projectileSpawnPoint.position;
    }
}
