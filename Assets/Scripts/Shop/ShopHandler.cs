using TMPro;
using UnityEditor;
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
    [SerializeField] TextMeshProUGUI hpRestoreLevelText;
    [SerializeField] TextMeshProUGUI critChanceLevelText;
    [SerializeField] TextMeshProUGUI totalCoinGainedLevelText;

    [Header("Upgrade Cost Text")]
    [SerializeField] TextMeshProUGUI maxHealthLevelCostText;
    [SerializeField] TextMeshProUGUI damageLevelCostText;
    [SerializeField] TextMeshProUGUI attackSpeedCostText;
    [SerializeField] TextMeshProUGUI hpRestoreLevelCostText;
    [SerializeField] TextMeshProUGUI critChanceLevelCostText;
    [SerializeField] TextMeshProUGUI totalCoinGainedLevelCostText;

    [Header("Coin Data")]
    [SerializeField] CoinCollected coinCollected;
    [SerializeField] float costMultiplier;

    private void Start()
    {
        Debug.Log("Load Shop Stats");
        InitializedUpgradeLevels();
        InitializeUpgradeCosts();
    }
    public void SaveUpgrades()
    {
        playerStatsConfig.SavePlayerConfig();
        //SaveManager.Instance.CallSaveGameMethod();
    }
    public void MaxHealthPoint()
    {
        if (!PurchaseBuyUpgrade(maxHealthLevelCostText)) return;

        // Directly modify player stats
        playerStatsConfig.ShopMaxHealthLevel++;
        playerStatsConfig.G_MaxHealthPoint++;

        maxHealthLevelText.text = SetLevelText(playerStatsConfig.ShopMaxHealthLevel);
    }

    public void Damage()
    {
        if (!PurchaseBuyUpgrade(damageLevelCostText)) return;

        playerStatsConfig.ShopDamageLevel++;
        playerStatsConfig.G_CobwebDamage++;

        damageLevelText.text = SetLevelText(playerStatsConfig.ShopDamageLevel);
    }

    public void AttackSpeed()
    {
        if (!PurchaseBuyUpgrade(attackSpeedCostText)) return;

        playerStatsConfig.ShopAttackSpeed++;
        playerStatsConfig.G_FireCooldown -= 0.1f;

        attackSpeedText.text = SetLevelText(playerStatsConfig.ShopAttackSpeed);
    }

    public void TotalCoinsGained()
    {
        if (!PurchaseBuyUpgrade(totalCoinGainedLevelCostText)) return;

        playerStatsConfig.ShopTotalCoinGainedLevel++;
        playerStatsConfig.G_CoinsMultiplier += 0.5f;

        totalCoinGainedLevelText.text = SetLevelText(playerStatsConfig.ShopTotalCoinGainedLevel);
    }

    public void CritRateChance()
    {
        if (!PurchaseBuyUpgrade(critChanceLevelCostText)) return;

        playerStatsConfig.ShopCritChanceLevel++;
        playerStatsConfig.G_CriticalChance += 5;

        critChanceLevelText.text = SetLevelText(playerStatsConfig.ShopCritChanceLevel);
    }

    public void HealthRestored()
    {
        if (!PurchaseBuyUpgrade(hpRestoreLevelCostText)) return;

        playerStatsConfig.ShopHpRestoreLevel++;
        playerStatsConfig.G_HealthToRestore++;

        hpRestoreLevelText.text = SetLevelText(playerStatsConfig.ShopHpRestoreLevel);
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
        maxHealthLevelText.text = SetLevelText(playerStatsConfig.ShopMaxHealthLevel);
        damageLevelText.text = SetLevelText(playerStatsConfig.ShopDamageLevel);
        attackSpeedText.text = SetLevelText(playerStatsConfig.ShopAttackSpeed);
        totalCoinGainedLevelText.text = SetLevelText(playerStatsConfig.ShopTotalCoinGainedLevel);
        critChanceLevelText.text = SetLevelText(playerStatsConfig.ShopCritChanceLevel);
        hpRestoreLevelText.text = SetLevelText(playerStatsConfig.ShopHpRestoreLevel);
    }
    private void InitializeUpgradeCosts()
    {
        // Load upgrade levels from the ScriptableObject (playerStatsConfig)
        int maxHealthLevel = playerStatsConfig.ShopMaxHealthLevel;
        int damageLevel = playerStatsConfig.ShopDamageLevel;
        int attackSpeedLevel = playerStatsConfig.ShopAttackSpeed;
        int totalCoinGainedLevel = playerStatsConfig.ShopTotalCoinGainedLevel;
        int critChanceLevel = playerStatsConfig.ShopCritChanceLevel;
        int hpRestoreLevel = playerStatsConfig.ShopHpRestoreLevel;

        // Calculate and update the cost text for each upgrade
        maxHealthLevelCostText.text = Mathf.RoundToInt(GetCostForLevel(maxHealthLevel)).ToString();
        damageLevelCostText.text = Mathf.RoundToInt(GetCostForLevel(damageLevel)).ToString();
        attackSpeedCostText.text = Mathf.RoundToInt(GetCostForLevel(attackSpeedLevel)).ToString();
        totalCoinGainedLevelCostText.text = Mathf.RoundToInt(GetCostForLevel(totalCoinGainedLevel)).ToString();
        critChanceLevelCostText.text = Mathf.RoundToInt(GetCostForLevel(critChanceLevel)).ToString();
        hpRestoreLevelCostText.text = Mathf.RoundToInt(GetCostForLevel(hpRestoreLevel)).ToString();
    }
    private float GetCostForLevel(int level)
    {
        int baseCost = 50; // Set your base cost here
        return baseCost * Mathf.Pow(costMultiplier, level);
    }
}
