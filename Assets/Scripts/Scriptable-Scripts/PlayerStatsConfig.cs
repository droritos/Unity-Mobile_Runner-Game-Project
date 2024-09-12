using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "ScriptableObject/Stats")] // You can now Create new Road file under "ScriptableObject"
public class PlayerStatsConfig : ScriptableObject
{
    [Header("Global Stats")]
    public int G_MaxHealthPoint = 2;
    public int G_CobwebDamage = 5;
    public float G_FireCooldown = 2.0f;
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

    [Header("In Shpp Upgrades Level")]
    public int MaxHealthLevel = 1;
    public int DamageLevel = 1;
    public int AttackSpeed = 1;
    public int TotalCoinGainedLevel = 1;
    public int CritChanceLevel = 1;
    public int HpRestoreLevel = 1;



    private void OnEnable()
    {
        InitializedStats();
    }

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
        //Debug.Log("Stats Initialized. e.g. - Fire Rate : " + FireCooldown);
    }


}
