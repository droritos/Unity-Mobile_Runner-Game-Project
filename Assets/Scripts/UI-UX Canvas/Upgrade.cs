using UnityEngine;

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
                    player.IncreasedAttaclSpeed(); // Increase attack speed by 20% per level
                    break;
                case "Restore Health":
                    player.RestoreHealth(20); // Restore 20 health per level
                    break;
                case "Ricochet":
                    player.Ricochet(); // Increase ricochet effect
                    break;
                case "Piercing":
                    player.PiercingLevel++; // Increase piercing effect
                    break;
                case "Increase Damage":
                    player.IncreasedDamage(2); // Increase damage by 2 per level
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
