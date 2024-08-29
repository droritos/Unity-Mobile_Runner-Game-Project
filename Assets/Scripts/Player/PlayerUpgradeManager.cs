using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    public int ExperiencePoints = 0;

    [Header("Leveling System")]
    [SerializeField] int baseExperienceToLevel = 100;
    [SerializeField] float experienceGrowthFactor = 1.5f;
    private int _currentLevel = 1;

    [Header("Upgrades")]
    [SerializeField] List<Upgrade> availableUpgrades;

    private PlayerBehavior playerBehavior;

    private void Start()
    {
        playerBehavior = GameManager.Instance.Player;
        InitializeUpgrades();
    }

    private void InitializeUpgrades()
    {
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

[System.Serializable]
public class Upgrade
{
    public string name;
    public string description;
    public Sprite icon;  // For UI representation
    public int maxLevel = 5;
    public int currentLevel = 0;

    // Method to apply the upgrade effect based on the current level
    public void ApplyUpgrade(PlayerBehavior player)
    {
        if (currentLevel < maxLevel)
        {
            switch (name)
            {
                case "Attack Speed":
                    player.AttackSpeedMultiplier += 0.2f; // Increase attack speed by 20% per level
                    break;
                case "Restore Health":
                    player.RestoreHealth(20); // Restore 20 health per level
                    break;
                case "Ricochet":
                    player.RicochetLevel++; // Increase ricochet effect
                    break;
                case "Piercing":
                    player.PiercingLevel++; // Increase piercing effect
                    break;
                case "Increase Damage":
                    player.CobwebDamage += 2; // Increase damage by 2 per level
                    break;
                case "Multi-Shot":
                    player.MultiShotLevel++; // Increase number of shots
                    break;
                case "Critical Hit Chance":
                    player.CriticalHitChance += 0.05f; // Increase critical hit chance by 5% per level
                    break;
                default:
                    Debug.LogWarning("Unknown upgrade: " + name);
                    break;
            }
            currentLevel++;
        }
    }

    public bool CanUpgrade()
    {
        return currentLevel < maxLevel;
    }
}
