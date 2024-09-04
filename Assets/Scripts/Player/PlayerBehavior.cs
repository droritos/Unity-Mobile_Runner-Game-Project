using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBehavior : MonoBehaviour
{
    [Header("Public Fields")]
    public PlayerArtitube playerArtitube;
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

    // This method will be called by an animation event on the player's Grabbed animation
    #endregion

    #region << Upgrade Methods - Buttons >> 
    public void RestoreHealth(int amountInPercentage)
    {
        playerArtitube.RestoreHealth(amountInPercentage);
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
