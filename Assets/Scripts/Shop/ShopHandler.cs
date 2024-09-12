using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    [Header("Player Stats Config Changer")]
    [SerializeField] PlayerStatsConfig playerStatsConfig;

    [Header("Upgrades Text Levels")]
    [SerializeField] TextMeshProUGUI maxHealthLevelText;
    [SerializeField] TextMeshProUGUI damageLevelText;
    [SerializeField] TextMeshProUGUI attackSpeedText;
    [SerializeField] TextMeshProUGUI totalCoinGainedLevelText;
    [SerializeField] TextMeshProUGUI critChanceLevelText;
    [SerializeField] TextMeshProUGUI hpRestoreLevelText;

    [Header("Upgrade Cost Text")]
    [SerializeField] TextMeshProUGUI maxHealthLevelCostText;
    [SerializeField] TextMeshProUGUI damageLevelCostText;
    [SerializeField] TextMeshProUGUI attackSpeedCostText;
    [SerializeField] TextMeshProUGUI totalCoinGainedLevelCostText;
    [SerializeField] TextMeshProUGUI critChanceLevelCostText;
    [SerializeField] TextMeshProUGUI hpRestoreLevelCostText;

    [Header("Coin Data")]
    [SerializeField] CoinCollected coinCollected;
    [SerializeField] float costMultiplier;

    private void Start()
    {
        InitializedUpgradeLevels();
    }

    public void MaxHealthPoint()
    {
        if (!PurchaseBuyUpgrade(maxHealthLevelCostText)) return;

        // Directly modify player stats
        playerStatsConfig.MaxHealthLevel++;
        playerStatsConfig.G_MaxHealthPoint++;

        maxHealthLevelText.text = SetLevelText(playerStatsConfig.MaxHealthLevel);
    }

    public void Damage()
    {
        if (!PurchaseBuyUpgrade(damageLevelCostText)) return;

        playerStatsConfig.DamageLevel++;
        playerStatsConfig.G_CobwebDamage++;

        damageLevelText.text = SetLevelText(playerStatsConfig.DamageLevel);
    }

    public void AttackSpeed()
    {
        if (!PurchaseBuyUpgrade(attackSpeedCostText)) return;

        playerStatsConfig.AttackSpeed++;
        playerStatsConfig.G_FireCooldown -= 0.1f;

        attackSpeedText.text = SetLevelText(playerStatsConfig.AttackSpeed);
    }

    public void TotalCoinsGained()
    {
        if (!PurchaseBuyUpgrade(totalCoinGainedLevelCostText)) return;

        playerStatsConfig.TotalCoinGainedLevel++;
        playerStatsConfig.G_CoinsMultiplier += 0.5f;

        totalCoinGainedLevelText.text = SetLevelText(playerStatsConfig.TotalCoinGainedLevel);
    }

    public void CritRateChance()
    {
        if (!PurchaseBuyUpgrade(critChanceLevelCostText)) return;

        playerStatsConfig.CritChanceLevel++;
        playerStatsConfig.G_CriticalChance += 5;

        critChanceLevelText.text = SetLevelText(playerStatsConfig.CritChanceLevel);
    }

    public void HealthRestored()
    {
        if (!PurchaseBuyUpgrade(hpRestoreLevelCostText)) return;

        playerStatsConfig.HpRestoreLevel++;
        playerStatsConfig.G_HealthToRestore++;

        hpRestoreLevelText.text = SetLevelText(playerStatsConfig.HpRestoreLevel);
    }

    private bool PurchaseBuyUpgrade(TextMeshProUGUI upgradeCostText)
    {
        int currentCost = int.Parse(upgradeCostText.text);

        if (coinCollected.TryBuyUpgrade(currentCost))
        {
            int newCost = Mathf.RoundToInt(currentCost * costMultiplier);
            upgradeCostText.text = newCost.ToString();
            return true;
        }
        else
        {
            Handheld.Vibrate();
            return false;
        }
    }

    private string SetLevelText(int level)
    {
        return $"LVL: {level}";
    }

    private void InitializedUpgradeLevels()
    {
        maxHealthLevelText.text = SetLevelText(playerStatsConfig.MaxHealthLevel);
        damageLevelText.text = SetLevelText(playerStatsConfig.DamageLevel);
        attackSpeedText.text = SetLevelText(playerStatsConfig.AttackSpeed);
        totalCoinGainedLevelText.text = SetLevelText(playerStatsConfig.TotalCoinGainedLevel);
        critChanceLevelText.text = SetLevelText(playerStatsConfig.CritChanceLevel);
        hpRestoreLevelText.text = SetLevelText(playerStatsConfig.HpRestoreLevel);
    }
}
