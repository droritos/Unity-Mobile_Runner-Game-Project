using System.Collections;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Public Fields")]
    public bool IsAlive = true;
    public int ExperiencePoints = 0;
    public int CobwebDamage = 5;
    [HideInInspector] public int coins = 0;

    [Header("Private Editable Fields")]
    [SerializeField] int maxHealthPoint = 2;
    [SerializeField] ObjectPoolManager coinPool;


    [Header("Local Fields")]
    private int _currentHP;
    private int _currentLevel;

    private void Start()
    {
        _currentHP = maxHealthPoint;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinPool.ReleaseObject(other.gameObject);
            coins++;
        }
        else if (other.CompareTag("EnemyProjectile"))
        {
            TakeDamage();
            GameManager.Instance.BulletPool.ReleaseObject(other.gameObject);
        }
    }

    private void TakeDamage()
    {
        _currentHP -= 1;
        if (_currentHP <= 0)
        {
            Die();
        }
    }

    #region << Dying Methods >> 
    private void Die()
    {
        IsAlive = false;
        // Die Animtaion 
        // Move to next scene 
        Debug.Log("You Died, Loser!");
    }

    // This method will be called by an animation event on the player's Grabbed animation
    #endregion
}
