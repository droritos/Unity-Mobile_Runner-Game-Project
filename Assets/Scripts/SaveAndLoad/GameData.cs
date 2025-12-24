
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool IsPlayerSurvived;

    public int PlayerCurrentLevel;
    public int CoinsCollected;
    public int CurrentHealhPoint;

    public int BaseExperienceToLevel;
    public float ExperienceGrowthFactor;
    public int ExperiencePoints;

    public float PlayerPositionX;

    public int MaxHealthPoint;
    public int CobwebDamage;
    public float FireCooldown;
    public float CobwebScaler;
    public int CobwebPiercingLevel;
    public int CriticalChance;


    public int MaxHealthLevelShop;
    public int DamageLevelShop;
    public int AttackSpeedShop;
    public int TotalCoinGainedLevelShop;
    public int CritChanceLevelShop;
    public int HpRestoreLevelShop;


    public float G_CoinsMultiplier;
    public int G_HealthToRestore;
    public int G_MaxHealthPoint;
    public int G_CobwebDamage;
    public float G_FireCooldown;
    public float G_CobwebScaler;
    public int G_CobwebPiercingLevel;
    public int G_CriticalChance;

    public GameData()
    {
        MaxHealthLevelShop = 1;
        DamageLevelShop = 1;
        AttackSpeedShop = 1;
        TotalCoinGainedLevelShop = 1;
        CritChanceLevelShop = 1;
        HpRestoreLevelShop = 1;

        G_CoinsMultiplier = 1;
        G_HealthToRestore = 2;
        G_MaxHealthPoint = 20;
        G_CobwebDamage = 50;
        G_FireCooldown = 1.5f;
        G_CobwebScaler = 0;
        G_CobwebPiercingLevel = 0;
        G_CriticalChance = 10;
    }
}
