using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBehavior : MonoBehaviour
{
    [Header("Public Fields")]
    public PlayerArtitube playerArtitube;
    [HideInInspector] public int coins = 0;

    [Header("Player Stats")]
    public int CobwebDamage = 5;
    public float AttackSpeedMultiplier = 1.0f;
    public int MultiShotLevel = 0;
    public float CriticalHitChance = 0.05f;
    public float CobwebScaler = 0;
    public int CobwebPiercingLevel = 0;
    public int CriticalChance = 10;

    [Header("In Game Upgrade Levels")]
    private int _piercingLevel = 1;
    private int _cobwebDamageLevel = 1;
    private int _cobwebScaleLevel = 1;
    private int _attackSpeedLevel = 1;

    [Header("Private Editable Fields")]
    [SerializeField] ObjectPoolManager coinPool;
    [SerializeField] ObjectPoolManager lvlUpPool;

    [Header("Local Fields")]
    private int _currentHP;
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
            GameManager.Instance.UpgradeMenuScript.HandleLevelUp();
        }
        else if (other.CompareTag("EnemyProjectile"))
        {
            playerArtitube.TakeDamage(1);
            GameManager.Instance.BulletPool.ReleaseObject(other.gameObject);
        }
    }
    #region << Dying Methods >> 
    public int LevelReachWhenDied()
    {
        if (!playerArtitube.IsAlive())
        {
            return playerArtitube.GetLevel();
        }
        else
        {
            return -1;
        }
    }
    #endregion

    public int ApplyDamage()
    {
        int chance = Random.Range(0, 100);
        if (chance >= CriticalChance)
        {
            Debug.Log($"U did Crit nice one!");
            return CobwebDamage * 2;
        }
        else
        {
            return CobwebDamage;
        }
    }


    #region << Upgrade Methods - Buttons >> 
    public void RestoreHealth(int amountInPercentage)
    {
        playerArtitube.RestoreHealth(amountInPercentage);
    }

    public void IncreasedDamage(TextMeshProUGUI levelText)
    {
        CobwebDamage += 2;
        _cobwebDamageLevel++;
        levelText.text = $"LVL : {_cobwebDamageLevel.ToString()}";
    }

    public void IncreasedAttackSpeed(TextMeshProUGUI levelText)
    {
        GameManager.Instance.SpidyMorals.FireCooldown -= 0.2f;
        _attackSpeedLevel++;
        levelText.text = $"LVL : {_attackSpeedLevel.ToString()}";

    }

    public void IncreaseWebSize(TextMeshProUGUI levelText)
    {
        CobwebScaler += 30f;
        _cobwebScaleLevel++;
        levelText.text = $"LVL : {_cobwebScaleLevel.ToString()}";

    }
    public void Piercing(TextMeshProUGUI levelText)
    {
        CobwebPiercingLevel++;
        _piercingLevel++;
        levelText.text = $"LVL : {_piercingLevel.ToString()}";
    }
    #endregion

}
