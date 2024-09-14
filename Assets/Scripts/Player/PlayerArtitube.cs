using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// PlayerArtitube is managing the player health and level,
/// and this script should be on the player prefab
/// </summary>
public class PlayerArtitube : MonoBehaviour
{
    public int PlayerCurrentLevel = 1;
    public int CurrentHealhPoint;
    public int ExperiencePoints = 0;


    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI healthSliderText;

    [Header("Experience")]
    [SerializeField] Slider experienceSlider;
    [SerializeField] TextMeshProUGUI experienceSliderText;
    [SerializeField] TextMeshProUGUI levelText;

    private PlayerStatsConfig _playerStatsConfig;
    private bool _isAlive = true;

    private void Start()
    {
        this._playerStatsConfig = GameManager.Instance.Player.PlayerStatsConfig;
        SetHealthPoint();
        SetExperiencePoints();
        ChangeHPSliderValue(); 
    }
    #region << Health Managing >>
    public void SetHealthPoint()
    {
        CurrentHealhPoint = _playerStatsConfig.MaxHealthPoint;
        SetMaxHealthBar();
        Debug.Log($"Current HP : {CurrentHealhPoint} | Map HP By Global {_playerStatsConfig.G_MaxHealthPoint}");
    }

    public void TakeDamage(int damage)
    {
        CurrentHealhPoint -= damage;
        CurrentHealhPoint = Mathf.Clamp(CurrentHealhPoint, 0, _playerStatsConfig.MaxHealthPoint);
        ChangeHPSliderValue();
        if (CurrentHealhPoint <= 0 || healthSlider.value == healthSlider.minValue)
        {
            Die();
        }
    }
    public void RestoreHealth()
    {
        // Add the restored health to the current health
        CurrentHealhPoint += _playerStatsConfig.HealthToRestore;

        // Ensure the health doesn't exceed the maximum health
        CurrentHealhPoint = Mathf.Clamp(CurrentHealhPoint, 0, _playerStatsConfig.MaxHealthPoint);

        // Update the health slider based on the new health percentage
        ChangeHPSliderValue();
        Debug.Log($"Restoring {_playerStatsConfig.HealthToRestore} to your health");
    }
    public bool IsAlive()
    {
        return _isAlive;
    }
    private void ChangeHPSliderValue()
    {
        // Calculate the health percentage and update the slider
        float healthPercentage = (float)CurrentHealhPoint / (float)_playerStatsConfig.MaxHealthPoint;
        healthSlider.value = healthPercentage; // Value is between 0 and 1
        UpdateHealthText();
    }
    private void SetMaxHealthBar()
    {
        healthSlider.value = 1;
        healthSlider.maxValue = 1;
    }
    public void UpdateHealthText()
    {
        healthSliderText.text = CurrentHealhPoint.ToString();
    }
    private void Die()
    {
        _isAlive = false;
        AddCollectedCoins(_playerStatsConfig.CoinsMultiplier);
        SceneManager.LoadScene(3);
        SaveManager.Instance.DeleteFileSavedFile();
        Debug.Log("You Died, Loser!");
    }

    private static void AddCollectedCoins(float multiplyCoins)
    {
        // Ensure multiplier is at least 1
        multiplyCoins = Mathf.Max(1f, multiplyCoins);

        int currentCoins = PlayerPrefs.GetInt("PlayerCoins", 0); // Get total coins from PlayerPrefs
        int coinsCollectedThisSession = ScoreManager.Instance.GetCoinsCollected(); // Coins collected this session

        // Apply multiplier and round to nearest int
        coinsCollectedThisSession = Mathf.RoundToInt(coinsCollectedThisSession * multiplyCoins);
        PlayerPrefs.SetInt("CoinsFromThisSessin", coinsCollectedThisSession);

        // Add to total coins
        int updatedTotalCoins = currentCoins + coinsCollectedThisSession;
        PlayerPrefs.SetInt("PlayerCoins", updatedTotalCoins); // Save the updated total

        PlayerPrefs.Save(); // Ensure the data is persisted immediately

        // Optional Debug Log
        Debug.Log($"Coins Collected: {coinsCollectedThisSession}, Multiplier: {multiplyCoins}, Total Coins: {updatedTotalCoins}");
    }
    #endregion

    #region << Experience Managing >>
    public void UpdateExperienceSlider(float experiencePercentage)
    {
        experienceSlider.value = experiencePercentage; // Value should be between 0 and 1
        UpdateExperienceText();
    }
    public int GetLevel()
    {
        return PlayerCurrentLevel;
    }
    public void LevelUp()
    {
        PlayerCurrentLevel++;
        UpdateLevelText();
        SaveManager.Instance.CallSaveGameMethod();
    }

    public void UpdateLevelText()
    {
        levelText.text = PlayerCurrentLevel.ToString();
    }

    private void SetExperiencePoints()
    {
        experienceSlider.value = 0;
        experienceSlider.maxValue = 1;
        UpdateExperienceText();
    }
    public void UpdateExperienceText()
    {
        experienceSliderText.text = $"{Mathf.RoundToInt(100 * experienceSlider.value)}%";
    }

    #endregion


}
