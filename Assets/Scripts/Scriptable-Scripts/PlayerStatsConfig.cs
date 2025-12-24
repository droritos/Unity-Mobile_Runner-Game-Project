using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "ScriptableObject/Stats")] // You can now Create new Road file under "ScriptableObject"
public class PlayerStatsConfig : ScriptableObject
{
    [Header("Global Stats")]
    public int G_MaxHealthPoint = 20;
    public int G_CobwebDamage = 50;
    public float G_FireCooldown = 1.5f;
    public float G_CobwebScaler = 0;
    public int G_CobwebPiercingLevel = 0;
    public int G_CriticalChance = 10;
    public int G_HealthToRestore = 2;
    public float G_CoinsMultiplier = 1;

    [Header("Local Stats")]
    [NonSerialized] public int MaxHealthPoint;
    [NonSerialized] public int CobwebDamage;
    [NonSerialized] public float FireCooldown;
    [NonSerialized] public float CobwebScaler;
    [NonSerialized] public int CobwebPiercingLevel;
    [NonSerialized] public int CriticalChance;
    [NonSerialized] public int HealthToRestore;
    [NonSerialized] public float CoinsMultiplier;

    [Header("In Shop Upgrades Level")]
    public int ShopMaxHealthLevel = 1;
    public int ShopDamageLevel = 1;
    public int ShopAttackSpeed = 1;
    public int ShopTotalCoinGainedLevel = 1;
    public int ShopCritChanceLevel = 1;
    public int ShopHpRestoreLevel = 1;
    private void OnEnable()
    {
        InitializedStats();
    }
    public void SavePlayerConfig()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.CallSaveGameMethod();
            Debug.Log("SO Called - CallSaveGameMethod");
        }
    }
    public void SetStats()
    {
        InitializedStats();
    }
    public void ResetStats()
    {
        G_MaxHealthPoint = 20;
        G_CobwebDamage = 50;
        G_FireCooldown = 1.5f;
        G_CobwebScaler = 0;
        G_CobwebPiercingLevel = 0;
        G_CriticalChance = 10;
        G_HealthToRestore = 2;
        G_CoinsMultiplier = 1;

        ShopMaxHealthLevel = 1;
        ShopDamageLevel = 1;
        ShopAttackSpeed = 1;
        ShopTotalCoinGainedLevel = 1;
        ShopCritChanceLevel = 1;
        ShopHpRestoreLevel = 1;
        Debug.Log("Stats Has Been Reset");
    }


    //public void Save(ref GameData data)
    //{
    //    data.MaxHealthPoint = G_MaxHealthPoint;
    //    data.CobwebDamage = G_CobwebDamage;
    //    data.FireCooldown = G_FireCooldown;
    //    data.CriticalChance = G_CriticalChance;
    //    data.CoinsMultiplier = G_CoinsMultiplier;
    //    data.HealthToRestore = G_HealthToRestore;

    //    data.MaxHealthLevelShop = ShopMaxHealthLevel;
    //    data.AttackSpeedShop = ShopAttackSpeed;
    //    data.CritChanceLevelShop = ShopCritChanceLevel;
    //    data.HpRestoreLevelShop = ShopHpRestoreLevel;
    //    data.DamageLevelShop = ShopDamageLevel;
    //    data.TotalCoinGainedLevelShop = ShopTotalCoinGainedLevel;
    //    Debug.Log($"Max HP Global: {G_MaxHealthPoint} |  From GameData {data.MaxHealthPoint} | In Shop Level {ShopMaxHealthLevel} From GD: {data.MaxHealthLevelShop}");
    //}

    //public void Load(GameData data)
    //{
    //    G_MaxHealthPoint = data.MaxHealthPoint;
    //    G_CobwebDamage = data.CobwebDamage;
    //    G_FireCooldown = data.FireCooldown;
    //    G_CriticalChance = data.CriticalChance;
    //    G_CoinsMultiplier = data.CoinsMultiplier;
    //    G_HealthToRestore = data.HealthToRestore;

    //    ShopMaxHealthLevel = data.MaxHealthLevelShop;
    //    ShopAttackSpeed = data.AttackSpeedShop;
    //    ShopCritChanceLevel = data.CritChanceLevelShop;
    //    ShopHpRestoreLevel = data.HpRestoreLevelShop;
    //    ShopDamageLevel = data.DamageLevelShop;
    //    ShopTotalCoinGainedLevel = data.TotalCoinGainedLevelShop;
    //    Debug.Log($"Max HP Global: {G_MaxHealthPoint} |  From GameData {data.MaxHealthPoint} | In Shop Level {ShopMaxHealthLevel} From GD: {data.MaxHealthLevelShop}");
    //}
    private void InitializedStats()
    {
        MaxHealthPoint = G_MaxHealthPoint;
        CobwebDamage = G_CobwebDamage;
        FireCooldown = G_FireCooldown;
        CobwebScaler = G_CobwebScaler;
        CobwebPiercingLevel = G_CobwebPiercingLevel;
        CriticalChance = G_CriticalChance;
        HealthToRestore = G_HealthToRestore;
        CoinsMultiplier = G_CoinsMultiplier;
    }

    //private float SetStatValue(float stat, int level)
    //{
    //    return (stat * level);  
    //}
    //private void UpdateStatsValue() // Deafult Stats * Level
    //{
    //    SetStatValue(2, ShopMaxHealthLevel);
    //    SetStatValue(5, ShopDamageLevel);
    //    SetStatValue(2.0f, ShopAttackSpeed);
    //    SetStatValue(10, ShopCritChanceLevel);
    //    SetStatValue(2, ShopHpRestoreLevel);
    //    SetStatValue(1, ShopTotalCoinGainedLevel);
    //}
}
