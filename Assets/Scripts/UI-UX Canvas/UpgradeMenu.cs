using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Leveling System")]
    [SerializeField] private int baseExperienceToLevel = 100;
    [SerializeField] private float experienceGrowthFactor = 1.5f;
    [SerializeField] UpgradeMenuAnimator  upgradeMenuAnimator;

    [Header("Upgrades")]
    [SerializeField] private Transform upgradesParent;

    private PlayerVitals _vitals;

    private void Start()
    {
        _vitals = GameManager.Instance.PlayerManager.PlayerBehavior.playerVitals;

        upgradesParent.gameObject.SetActive(false);

        // Force UI refresh through events (without changing XP)
        RaiseXPUI();
    }

    /// <summary>
    /// Adding the amount of Exprerince
    /// </summary>
    /// <param name="amount">If using -1 applying Level up</param>
    public void GainExperience(int amount = -1)
    {
        if (_vitals == null) return;

        int required = GetRequiredXPForLevel(_vitals.Level);
        
        if(amount <= -1) // Insta Level Up
            amount = required;
        
        _vitals.AddXP(amount,required);

        // 🔥 UI update now happens by event (not direct UI calls)
        _vitals.SetXPPercent((float)_vitals.ExperiencePoints / required);

        if (_vitals.ExperiencePoints >= required)
            HandleLevelUp();
    }

    private void HandleLevelUp()
    {
        int required = GetRequiredXPForLevel(_vitals.Level);

        _vitals.ResetXP();

        _vitals.LevelUp();

        ScoreManager.Instance.AddToScore(250);
        ShowUpgradeOptions();
    }

    private int GetRequiredXPForLevel(int level)
    {
        return Mathf.RoundToInt(baseExperienceToLevel * Mathf.Pow(experienceGrowthFactor, level - 1));
    }

    private void RaiseXPUI()
    {
        int required = GetRequiredXPForLevel(_vitals.Level);
        _vitals.SetXPPercent(required <= 0 ? 0f : (float)_vitals.ExperiencePoints / required);
    }
    [ContextMenu("Choose Upgrades")]
    private void ShowUpgradeOptions()
    {
        // 1. Pause World
        PauseManager.Instance.SetPaused(true);
        
        // 2. Reset: Hide all children first
        foreach (Transform child in upgradesParent)
            child.gameObject.SetActive(false);

        // 3. Pick 3 Random Cards
        List<Transform> activeCards = new List<Transform>();
        int activatedCount = 0;

        // Safety check to prevent infinite loop if you have < 3 upgrades total
        int maxAttempts = 100; 
        int attempts = 0;

        while (activatedCount < 3 && upgradesParent.childCount > 0 && attempts < maxAttempts)
        {
            attempts++;
            Transform randomChild = upgradesParent.GetChild(Random.Range(0, upgradesParent.childCount));
            
            if (!randomChild.gameObject.activeSelf)
            {
                randomChild.gameObject.SetActive(true);
                activeCards.Add(randomChild); // Add to our list for the animator
                activatedCount++;
            }
        }

        // 4. Show the Menu Parent
        upgradesParent.gameObject.SetActive(true);

        // 5. Trigger the Animation on ONLY the chosen cards
        if (upgradeMenuAnimator != null)
        {
            upgradeMenuAnimator.PlayOpenAnimation(activeCards);
        }
    }

    public void CloseMenu()
    {
        PauseManager.Instance.SetPaused(false);
        upgradeMenuAnimator.CloseMenu();
    }


}
