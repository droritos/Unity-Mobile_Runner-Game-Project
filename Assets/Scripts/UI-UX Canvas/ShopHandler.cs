using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] int maxHealthLevel;
    [SerializeField] int damageLevel;
    [SerializeField] int attackSpeed;
    [SerializeField] int totalCoinGainedLevel;
    [SerializeField] int critChanceLevel;
    
    private void TestTesty()
    {
        GameManager.Instance.Player.CobwebDamage = damageLevel;
        Debug.Log($"You accessing cobweb damage varible {damageLevel}");
    }
}
