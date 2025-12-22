using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePlayerStatsConfig : MonoBehaviour,ISavable
{
    [SerializeField] PlayerStatsConfig playerStatsConfig;
    public void Save(ref GameData data)
    {
        data.MaxHealthPoint = playerStatsConfig.G_MaxHealthPoint;
        data.CobwebDamage = playerStatsConfig.G_CobwebDamage;
        data.FireCooldown = playerStatsConfig.G_FireCooldown;
        data.CriticalChance = playerStatsConfig.G_CriticalChance;
        data.G_CoinsMultiplier = playerStatsConfig.G_CoinsMultiplier;
        data.G_HealthToRestore = playerStatsConfig.G_HealthToRestore;

        data.MaxHealthLevelShop = playerStatsConfig.ShopMaxHealthLevel;
        data.AttackSpeedShop = playerStatsConfig.ShopAttackSpeed;
        data.CritChanceLevelShop = playerStatsConfig.ShopCritChanceLevel;
        data.HpRestoreLevelShop = playerStatsConfig.ShopHpRestoreLevel;
        data.DamageLevelShop = playerStatsConfig.ShopDamageLevel;
        data.TotalCoinGainedLevelShop = playerStatsConfig.ShopTotalCoinGainedLevel;
        Debug.Log($"Max HP Global: {playerStatsConfig.G_MaxHealthPoint} |  From GameData {data.MaxHealthPoint} | In Shop Level {playerStatsConfig.ShopMaxHealthLevel} From GD: {data.MaxHealthLevelShop}");
    }

    public void Load(GameData data)
    {
        playerStatsConfig.G_MaxHealthPoint = data.MaxHealthPoint;
        playerStatsConfig.G_CobwebDamage = data.CobwebDamage;
        playerStatsConfig.G_FireCooldown = data.FireCooldown;
        playerStatsConfig.G_CriticalChance = data.CriticalChance;
        playerStatsConfig.G_CoinsMultiplier = data.G_CoinsMultiplier;
        playerStatsConfig.G_HealthToRestore = data.G_HealthToRestore;

        playerStatsConfig.ShopMaxHealthLevel = data.MaxHealthLevelShop;
        playerStatsConfig.ShopAttackSpeed = data.AttackSpeedShop;
        playerStatsConfig.ShopCritChanceLevel = data.CritChanceLevelShop;
        playerStatsConfig.ShopHpRestoreLevel = data.HpRestoreLevelShop;
        playerStatsConfig.ShopDamageLevel = data.DamageLevelShop;
        playerStatsConfig.ShopTotalCoinGainedLevel = data.TotalCoinGainedLevelShop;
        Debug.Log($"Max HP Global: {playerStatsConfig.G_MaxHealthPoint} |  From GameData {data.MaxHealthPoint} | In Shop Level {playerStatsConfig.ShopMaxHealthLevel} From GD: {data.MaxHealthLevelShop}");
    }
}
