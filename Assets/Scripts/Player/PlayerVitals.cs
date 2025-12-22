using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVitals : MonoBehaviour
{
    
    public int Level { get; private set; } = 1;
    public int CurrentHP { get; private set; }
    public int ExperiencePoints { get; private set; } = 0;

    public event Action<int, int> HPChanged;          // current, max
    public event Action<float> XPPercentChanged;      // 0..1
    public event Action<int> LevelChanged;            // new level
    public event Action Died;
    public event Action<int> OnCoinsGathered;

    private PlayerStatsConfig _stats;
    private bool _isAlive = true;

    public bool IsAlive => _isAlive;

    private void Start()
    {
        _stats = GameManager.Instance.Player.PlayerStatsConfig;

        ResetHP();
        ResetXPUI();
        RaiseCoinsGathered(0);
        RaiseAllUI();
    }

    public void ResetHP()
    {
        CurrentHP = _stats.MaxHealthPoint;
        HPChanged?.Invoke(CurrentHP, _stats.MaxHealthPoint);
    }

    public void TakeDamage(int damage)
    {
        if (!_isAlive) return;

        CurrentHP = Mathf.Clamp(CurrentHP - damage, 0, _stats.MaxHealthPoint);
        HPChanged?.Invoke(CurrentHP, _stats.MaxHealthPoint);

        if (CurrentHP <= 0)
            Die();
    }
    public void RaiseCoinsGathered(int amount)
    {
        OnCoinsGathered?.Invoke(amount);
    }
    public void RestoreHealth()
    {
        if (!_isAlive) return;

        CurrentHP = Mathf.Clamp(CurrentHP + _stats.HealthToRestore, 0, _stats.MaxHealthPoint);
        HPChanged?.Invoke(CurrentHP, _stats.MaxHealthPoint);
    }

    public void SetVitals(int level, int hp, int xp)
    {
        Level = level;
        CurrentHP = hp;
        SetXPPercent(xp);
        RaiseAllUI();
    }
    public void SetXPPercent(float percent01)
    {
        percent01 = Mathf.Clamp01(percent01);
        XPPercentChanged?.Invoke(percent01);
    }

    public void LevelUp()
    {
        Level++;
        LevelChanged?.Invoke(Level);
        SaveManager.Instance.CallSaveGameMethod();
    }

    private void ResetXPUI()
    {
        XPPercentChanged?.Invoke(0f);
        LevelChanged?.Invoke(Level);
    }

    private void RaiseAllUI()
    {
        HPChanged?.Invoke(CurrentHP, _stats.MaxHealthPoint);
        LevelChanged?.Invoke(Level);
    }

    private void Die()
    {
        if (!_isAlive) return;

        _isAlive = false;

        AddCollectedCoins(_stats.CoinsMultiplier);

        Died?.Invoke();                 // UI / audio can react
        EventManager.InvokeGameOver(this);

        SaveManager.Instance.DeleteFileSavedFile();
        SceneManager.LoadScene(3);
    }

    private static void AddCollectedCoins(float multiplyCoins)
    {
        multiplyCoins = Mathf.Max(1f, multiplyCoins);

        int currentCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        int coinsThisSession = ScoreManager.Instance.GetCoinsCollected();

        coinsThisSession = Mathf.RoundToInt(coinsThisSession * multiplyCoins);
        PlayerPrefs.SetInt("CoinsFromThisSessin", coinsThisSession);

        int updatedTotal = currentCoins + coinsThisSession;
        PlayerPrefs.SetInt("PlayerCoins", updatedTotal);
        PlayerPrefs.Save();
    }

    public void AddXP(int amount, int xpRequiredForNextLevel)
    {
        if (!_isAlive) return;

        ExperiencePoints += amount;
        ExperiencePoints = Mathf.Max(0, ExperiencePoints);

        float percent = (xpRequiredForNextLevel <= 0) ? 0f : (float)ExperiencePoints / xpRequiredForNextLevel;
        XPPercentChanged?.Invoke(Mathf.Clamp01(percent));
    }

    public void ResetXP()
    {
        ExperiencePoints = 0;
        XPPercentChanged?.Invoke(0f);
    }

}
