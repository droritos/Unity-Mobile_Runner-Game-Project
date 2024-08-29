using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBehavior : MonoBehaviour
{
    [Header("Public Fields")]
    public bool IsAlive = true;
    public int ExperiencePoints = 0;
    public int CobwebDamage = 5;
    public float AttackSpeedMultiplier = 1.0f;
    public int RicochetLevel = 0;
    public int PiercingLevel = 0;
    public int MultiShotLevel = 0;
    public float CriticalHitChance = 0.05f;
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
        IsAlive = false; // Notice! : When you die its stop the score leveling
        SceneManager.LoadScene(3);
        // Die Animtaion 
        // Move to next scene 
        Debug.Log("You Died, Loser!");
    }

    // This method will be called by an animation event on the player's Grabbed animation
    #endregion

    #region << Upgrade Methods >> 
    public void RestoreHealth(int amount)
    {
        _currentHP = Mathf.Min(_currentHP + amount, maxHealthPoint);
    }
    #endregion

}
