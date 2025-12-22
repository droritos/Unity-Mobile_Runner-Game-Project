using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Leveling System")]
    [SerializeField] private int baseExperienceToLevel = 100;
    [SerializeField] private float experienceGrowthFactor = 1.5f;

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

    private void ShowUpgradeOptions()
    {
        foreach (Transform child in upgradesParent)
            child.gameObject.SetActive(false);

        int activatedCount = 0;
        while (activatedCount < 3 && upgradesParent.childCount > 0)
        {
            Transform randomChild = upgradesParent.GetChild(Random.Range(0, upgradesParent.childCount));
            if (!randomChild.gameObject.activeSelf)
            {
                randomChild.gameObject.SetActive(true);
                activatedCount++;
            }
        }

        upgradesParent.gameObject.SetActive(true);
    }
}
