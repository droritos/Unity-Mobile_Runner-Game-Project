using UnityEngine;

public class RobotEnemyScript : MonoBehaviour
{
    [SerializeField] GameObject experienceParticals;
    [SerializeField] GameObject bullet;
    [SerializeField] int health = 10;
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        
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

    private void Shoot()
    {

    }

    private void Die()
    {

    }
}
