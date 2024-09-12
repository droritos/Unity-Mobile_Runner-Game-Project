using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradation
{
    public int CurrentLevel { get; private set; }
    private int baseCost;
    private float costMultiplier;

    public Upgradation(int baseCost, float costMultiplier, int startingLevel = 0)
    {
        this.baseCost = baseCost;
        this.costMultiplier = costMultiplier;
        this.CurrentLevel = startingLevel;
    }

    // Get the current cost based on the level
    public int GetUpgradeCost()
    {
        return Mathf.RoundToInt(baseCost * Mathf.Pow(costMultiplier, CurrentLevel));
    }

    // Purchase the upgrade and increment the level
    public void PurchaseUpgrade()
    {
        CurrentLevel++;
    }
}
