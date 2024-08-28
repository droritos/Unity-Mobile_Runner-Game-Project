using UnityEngine;
using System.Collections;

public class RobotEnemyScript : MonoBehaviour
{
    [Header("Enemy Fields")]
    [SerializeField] int health = 10;
    [SerializeField] float fireColdown = 1f;
    [SerializeField] float hitDuration = 0.1f; // Duration to keep the object red
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] GameObject bullet;

    [Header("Enemy Pools")]
    public ObjectPoolManager EnemyPool;
    [SerializeField] ObjectPoolManager bulletPool;

    [Header("Enemy Visual")]
    [SerializeField] Transform robotGFX;

    private float _fire = 0;
    private Animator _animator;
    private Renderer objectRenderer;

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
            Destroy(other.gameObject);
        }
    }
    public void EnemyPooled(/*Vector3 movePosition, int speed*/)
    {
        GameObject enemy = EnemyPool.GetObject();
    }


    public void TakeDamage(int damage)
    {
        // Decrease health or dead
        health -= damage;
        StartCoroutine(HitAnimation());

        if (health <= 0)
        {
            Die();
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
            bulletPool.IsMaxPoolSize(projectileSpawnPoint.position);
        }
    }

    private void Die()
    {
        // player gets experience points
        //enemyPool.ReleaseObject(parent);
    }

    private IEnumerator WaitForShoot()
    {
        // Wait until animation is done
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        // Attempt to get an object from the pool
        bullet = bulletPool.GetObject();
        bullet.transform.position = projectileSpawnPoint.position;
        bulletPool.ActiveObjects.Enqueue(bullet);
    }
}
