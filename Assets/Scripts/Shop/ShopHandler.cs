using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] int maxHealthLevel = 1;
    [SerializeField] int damageLevel = 1;
    [SerializeField] int attackSpeed = 1;
    [SerializeField] int totalCoinGainedLevel = 1;
    [SerializeField] int critChanceLevel = 1;
    [SerializeField] int hpRestoreLevel = 1;    
    
    public void MaxHealthPoint()
    {
        maxHealthLevel++;
    }

    public void Damage()
    {
        damageLevel ++;
    }

    public void AttackSpeed()
    {
        attackSpeed++;
        GameManager.Instance.SpidyMorals.FireCooldown -= 0.1f;
    }

    public void TotalCoinsGained()
    {
        totalCoinGainedLevel++;
        // Need to make a method for that 
    }

    public void CritRateChance()
    {
        critChanceLevel++;
    }

}
