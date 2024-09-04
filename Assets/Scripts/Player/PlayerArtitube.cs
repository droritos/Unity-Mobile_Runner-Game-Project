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
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] int maxHealthPoint = 2;
    [SerializeField] TextMeshProUGUI healthSliderText;

    [Header("Experience")]
    [SerializeField] Slider experienceSlider;
    [SerializeField] TextMeshProUGUI experienceSliderText;
    [SerializeField] TextMeshProUGUI levelText;

    private int _playerCurrentLevel = 1;
    private int _currentHealhPoint;
    private bool _isAlive = true;

    private void Start()
    {
        SetHealthPoint();
        SetExperiencePoints();
    }
    #region << Health Managing >>
    public void SetHealthPoint()
    {
        _currentHealhPoint = maxHealthPoint;
        SetMaxHealthBar();
    }

    public void TakeDamage(int damage)
    {
        _currentHealhPoint -= damage;
        _currentHealhPoint = Mathf.Clamp(_currentHealhPoint, 0, maxHealthPoint);
        ChangeHPSliderValue();
        if (_currentHealhPoint <= 0 || healthSlider.value == healthSlider.minValue)
        {
            Die();
        }
    }
    public void RestoreHealth(float healthRestorePercentage)
    {
        // Calculate the health points to restore based on the percentage
        healthRestorePercentage /= 100;
        int healthToRestore = Mathf.FloorToInt(maxHealthPoint * healthRestorePercentage);
        Debug.Log($"health to restore {healthToRestore}");

        // Add the restored health to the current health
        _currentHealhPoint += healthToRestore;

        // Ensure the health doesn't exceed the maximum health
        _currentHealhPoint = Mathf.Clamp(_currentHealhPoint, 0, maxHealthPoint);

        // Update the health slider based on the new health percentage
        ChangeHPSliderValue();
    }
    public bool IsAlive()
    {
        return _isAlive;
    }
    private void ChangeHPSliderValue()
    {
        // Calculate the health percentage and update the slider
        float healthPercentage = (float)_currentHealhPoint / (float)maxHealthPoint;
        healthSlider.value = healthPercentage; // Value is between 0 and 1
        UpdateHealthText();
    }
    private void SetMaxHealthBar()
    {
        healthSlider.value = 1;
        healthSlider.maxValue = 1;
    }
    private void UpdateHealthText()
    {
        healthSliderText.text = $"{100 * healthSlider.value}%";
    }
    private void Die()
    {
        _isAlive = false;
        SceneManager.LoadScene(3);
        Debug.Log("You Died, Loser!");
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
        return _playerCurrentLevel;
    }
    public void LevelUp()
    {
        _playerCurrentLevel++;
        levelText.text = _playerCurrentLevel.ToString();
    }
    private void SetExperiencePoints()
    {
        experienceSlider.value = 0;
        experienceSlider.maxValue = 1;
        UpdateExperienceText();
    }
    private void UpdateExperienceText()
    {
        experienceSliderText.text = $"{Mathf.RoundToInt(100 * experienceSlider.value)}%";
    }

    #endregion


}
