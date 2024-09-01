using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBehavior : MonoBehaviour
{
    [Header("Public Fields")]
    public bool IsAlive = true;
    public int ExperiencePoints = 0;
    public int CobwebDamage = 5;
    [HideInInspector] public int coins = 0;

    [Header("Upgrades Fields")]
    public float AttackSpeedMultiplier = 1.0f;
    public int MultiShotLevel = 0;
    public float CriticalHitChance = 0.05f;
    public float CobwebScaler = 0;
    public int CobwebPiercingLevel = 0;

    public int PiercingLevel = 1;
    public int CobwebDamageLevel = 1;
    public int CobwebScaleLevel = 1;
    public int AttackSpeedLevel = 1;



    [Header("Private Editable Fields")]
    [SerializeField] int maxHealthPoint = 2;
    [SerializeField] ObjectPoolManager coinPool;
    [SerializeField] ObjectPoolManager lvlUpPool;

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
        else if (other.CompareTag("LvLUp"))
        {
            lvlUpPool.ReleaseObject(other.gameObject);
            GameManager.Instance.UpgradeMenuScript.LevelUp();
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

    public int LevelReachWhenDied()
    {
        if (!IsAlive)
        {
            return GameManager.Instance.UpgradeMenuScript.CurrentLevel;
        }
        else
        {
            return -1;
        }
    }

    // This method will be called by an animation event on the player's Grabbed animation
    #endregion

    #region << Upgrade Methods - Buttons >> 
    public void RestoreHealth(int amount)
    {
        _currentHP = Mathf.Min(_currentHP + amount, maxHealthPoint);
    }

    public void IncreasedDamage(TextMeshProUGUI levelText)
    {
        CobwebDamage += 2;
        levelText.text = $"LVL : {CobwebDamageLevel.ToString()}";
    }

    public void IncreasedAttaclSpeed(TextMeshProUGUI levelText)
    {
        GameManager.Instance.SpidyMorals.FireCooldown -= 0.2f;
        levelText.text = $"LVL : {AttackSpeedLevel.ToString()}";

    }

    public void IncreaseWebSize(TextMeshProUGUI levelText)
    {
        CobwebScaler += 30f;
        CobwebScaleLevel++;
        levelText.text = $"LVL : {CobwebScaleLevel.ToString()}";

    }
    public void Piercing(TextMeshProUGUI levelText)
    {
        CobwebPiercingLevel++;
        levelText.text = $"LVL : {CobwebPiercingLevel.ToString()}";
    }
    #endregion

}
