using UnityEngine;
using System.Collections;


public class RobotEnemyScript : MonoBehaviour
{
    [SerializeField] GameObject experienceParticals;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] int health = 10;
    [SerializeField] float fireColdown = 1f;
    [SerializeField] ObjectPoolManager bulletPool;
    private float _fire = 0;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        ShootProjectile();
    }

    
    public void TakeDamage(int damage)
    {
        //descrese health or dead
        health  -= damage;
        if (health <= 0)
        {
            // drops exp 
            Destroy(gameObject);
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
            bulletPool.IsMaxPoolSize(spawnPoint.position);

        }
    }


    private void Die()
    {

    }

    private IEnumerator WaitForShoot()
    {
        // Wait until animation is done
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        // Attempt to get an object from the pool
        bullet = bulletPool.GetObject();
        bulletPool.ActiveObjects.Enqueue(bullet);
    }

}
