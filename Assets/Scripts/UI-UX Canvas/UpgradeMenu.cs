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
    private int _experiencePoints = 0;

    private void Start()
    {
        _playerBehavior = GameManager.Instance.Player;
        upgradesParent.gameObject.SetActive(false);
    }

    public void GainExperience(int amount)
    {
        _experiencePoints += amount;
        int experienceRequired = Mathf.RoundToInt(baseExperienceToLevel * Mathf.Pow(experienceGrowthFactor, _playerBehavior.playerArtitube.GetLevel() - 1));

        // Update the experience slider as EXP is gained
        UpdateExperienceSlider(experienceRequired);

        if (_experiencePoints >= experienceRequired)
        {
            HandleLevelUp();
        }
    }

    public void HandleLevelUp()
    {
        _experiencePoints = 0;
        _playerBehavior.playerArtitube.LevelUp();
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
        if (_playerBehavior.playerArtitube != null)
        {
            if (experienceRequired == -1)
            {
                experienceRequired = Mathf.RoundToInt(baseExperienceToLevel * Mathf.Pow(experienceGrowthFactor, _playerBehavior.playerArtitube.GetLevel() - 1));
            }

            // Update the slider value in PlayerArtitube based on current EXP progress
            _playerBehavior.playerArtitube.UpdateExperienceSlider((float)_experiencePoints / experienceRequired);
        }
    }
}

