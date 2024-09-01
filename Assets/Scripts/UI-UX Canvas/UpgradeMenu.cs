using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public int ExperiencePoints = 0;
    public int CurrentLevel = 1;

    [Header("Leveling System")]
    [SerializeField] int baseExperienceToLevel = 100;
    [SerializeField] float experienceGrowthFactor = 1.5f;

    [Header("Upgrades")]
    [SerializeField] Transform upgradesParent;

    private PlayerBehavior playerBehavior;

    private void Start()
    {
        playerBehavior = GameManager.Instance.Player;
        upgradesParent.gameObject.SetActive(false);
    }

    public void GainExperience(int amount)
    {
        ExperiencePoints += amount;
        Debug.Log($"Gained EXP {ExperiencePoints}");
        int experienceRequired = Mathf.RoundToInt(baseExperienceToLevel * Mathf.Pow(experienceGrowthFactor, CurrentLevel - 1));

        if (ExperiencePoints >= experienceRequired)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        ExperiencePoints = 0;
        CurrentLevel++;
        ScoreManager.Instance.AddToScore(250);
        ShowUpgradeOptions();
    }

    public int GetLevel()
    {
        return CurrentLevel;
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

