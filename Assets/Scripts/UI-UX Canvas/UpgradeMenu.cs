using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Leveling System")]
    [SerializeField] int baseExperienceToLevel = 100;
    [SerializeField] float experienceGrowthFactor = 1.5f;

    [Header("Upgrades")]
    [SerializeField] Transform upgradesParent;

    private PlayerBehavior _playerBehavior;

    private void Start()
    {
        _playerBehavior = GameManager.Instance.PlayerManager.PlayerBehavior;
        upgradesParent.gameObject.SetActive(false);
        GainExperience(0); // Easy way to update the UI when you Resume - (_playerBehavior.Load())
    }

    public void GainExperience(int amount)
    {
        _playerBehavior.PlayerArtitube.ExperiencePoints += amount;
        int experienceRequired = Mathf.RoundToInt(baseExperienceToLevel * Mathf.Pow(experienceGrowthFactor, _playerBehavior.PlayerArtitube.GetLevel() - 1));

        // Update the experience slider as EXP is gained
        UpdateExperienceSlider(experienceRequired);

        if (_playerBehavior.PlayerArtitube.ExperiencePoints >= experienceRequired)
        {
            HandleLevelUp();
        }
    }

    public void HandleLevelUp()
    {
        _playerBehavior.PlayerArtitube.ExperiencePoints = 0;
        _playerBehavior.PlayerArtitube.LevelUp();
        ScoreManager.Instance.AddToScore(250);
        ShowUpgradeOptions();
        UpdateExperienceSlider();
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
    private void UpdateExperienceSlider(int experienceRequired = -1)
    {
        if (_playerBehavior.PlayerArtitube != null)
        {
            if (experienceRequired == -1)
            {
                experienceRequired = Mathf.RoundToInt(baseExperienceToLevel * Mathf.Pow(experienceGrowthFactor, _playerBehavior.PlayerArtitube.GetLevel() - 1));
            }

            // Update the slider value in PlayerArtitube based on current EXP progress
            _playerBehavior.PlayerArtitube.UpdateExperienceSlider((float)_playerBehavior.PlayerArtitube.ExperiencePoints / experienceRequired);
        }
    }
}

