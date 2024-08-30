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
        upgradesParent.gameObject.SetActive(false);
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
        // First, deactivate all children to ensure a clean slate
        foreach (Transform child in upgradesParent)
        {
            child.gameObject.SetActive(false);
        }

        int activatedCount = 0;

        // Continue until 3 unique children are activated
        while (activatedCount < 3)
        {
            // Randomly select a child from upgradesParent
            Transform randomChild = upgradesParent.GetChild(Random.Range(0, upgradesParent.childCount));

            // Only activate the child if it is currently inactive
            if (!randomChild.gameObject.activeSelf)
            {
                randomChild.gameObject.SetActive(true);
                activatedCount++;
            }
        }

        // Make sure the upgradesParent is visible
        upgradesParent.gameObject.SetActive(true);
    }

}

