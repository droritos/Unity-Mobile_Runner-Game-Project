using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public int ExperiencePoints = 0;

    [Header("Leveling System")]
    [SerializeField] int baseExperienceToLevel = 100;
    [SerializeField] float experienceGrowthFactor = 1.5f;
    private int _currentLevel = 1;

    [Header("Upgrades")]
    [SerializeField] List<Upgrade> availableUpgrades;
    [SerializeField] Transform upgradesParent;

    private PlayerBehavior playerBehavior;

    private void Start()
    {
        playerBehavior = GameManager.Instance.Player;
        InitializeUpgrades();
    }

    private void InitializeUpgrades()
    {
        // Loop through each child of the upgradesParent
        foreach (Transform child in upgradesParent)
        {
            // Attempt to get the Upgrade component on the child
            Upgrade upgradeOption = child.GetComponent<Upgrade>();

            // If the child has an Upgrade component, add it to the list
            if (upgradeOption != null)
            {
                availableUpgrades.Add(upgradeOption);
            }
        }

        /*
        availableUpgrades = new List<Upgrade>
        {
            new Upgrade { name = "Attack Speed", description = "Increases the rate of fire." },
            new Upgrade { name = "Restore Health", description = "Restores some health.", maxLevel = 3 },
            new Upgrade { name = "Ricochet", description = "Web shots ricochet to another lane." },
            new Upgrade { name = "Piercing", description = "Web shots pierce through enemies." },
            new Upgrade { name = "Increase Damage", description = "Increases damage output." },
            new Upgrade { name = "Multi-Shot", description = "Shoots multiple web shots." },
            new Upgrade { name = "Critical Hit Chance", description = "Increases the chance of critical hits." }
        };
        */
    }

    public void GainExperience(int amount)
    {
        ExperiencePoints += amount;

        int experienceRequired = Mathf.RoundToInt(baseExperienceToLevel * Mathf.Pow(experienceGrowthFactor, _currentLevel - 1));

        if (ExperiencePoints >= experienceRequired)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        ExperiencePoints = 0;
        _currentLevel++;
        ShowUpgradeOptions();
    }

    private void ShowUpgradeOptions()
    {
        // Pick 3 random upgrades to offer to the player
        List<Upgrade> upgradesToShow = new List<Upgrade>();
        while (upgradesToShow.Count < 3)
        {
            Upgrade randomUpgrade = availableUpgrades[Random.Range(0, availableUpgrades.Count)];
            if (randomUpgrade.CanUpgrade() && !upgradesToShow.Contains(randomUpgrade))
            {
                upgradesToShow.Add(randomUpgrade);
            }
        }

        // Example: Automatically apply the first upgrade (you would normally let the player choose)
        ApplyUpgrade(upgradesToShow[0]);
    }

    public void ApplyUpgrade(Upgrade chosenUpgrade)
    {
        chosenUpgrade.ApplyUpgrade(playerBehavior);
    }


}

