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
    
    private void TestTesty()
    {
        GameManager.Instance.Player.CobwebDamage = damageLevel;
        Debug.Log($"You accessing cobweb damage varible {damageLevel}");
    }

    public void MaxHealthPoint()
    {
        Debug.Log($"Befor : {GameManager.Instance.Player.playerArtitube.MaxHealthPoint}");
        maxHealthLevel++;
        GameManager.Instance.Player.playerArtitube.MaxHealthPoint += maxHealthLevel;
        Debug.Log($"After : {GameManager.Instance.Player.playerArtitube.MaxHealthPoint}");
    }

    public void Damage()
    {
        damageLevel ++;
        GameManager.Instance.Player.CobwebDamage += damageLevel;
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
        GameManager.Instance.Player.CriticalChance += 10;
    }

}
