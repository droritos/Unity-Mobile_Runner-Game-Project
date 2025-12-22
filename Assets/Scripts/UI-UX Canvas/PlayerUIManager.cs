using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Image healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Experience")]
    [SerializeField] private Image xpSlider;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI levelText;
    
    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI coinsText;

    private const string LevelText = "Level: ";
    
    private PlayerVitals _playerVitals;
/*
    private void Awake()
    {
        // sliders use 0..1
        if (healthSlider != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = 1f;
        }

        if (xpSlider != null)
        {
            xpSlider.minValue = 0f;
            xpSlider.maxValue = 1f;
        }
    }
*/
    // Call this from GameManager when player spawns / is ready
    private void OnDestroy() => Unbind();
    public void Bind(PlayerVitals player)
    {
        Unbind();

        _playerVitals = player;
        if (_playerVitals == null) return;

        _playerVitals.HPChanged += OnHPChanged;
        _playerVitals.XPPercentChanged += OnXPChanged;
        _playerVitals.LevelChanged += OnLevelChanged;
        _playerVitals.Died += OnDied;
        _playerVitals.OnCoinsGathered += OnCoinsGathered;
    }

    private void Unbind()
    {
        if (_playerVitals == null) return;

        _playerVitals.HPChanged -= OnHPChanged;
        _playerVitals.XPPercentChanged -= OnXPChanged;
        _playerVitals.LevelChanged -= OnLevelChanged;
        _playerVitals.Died -= OnDied;
        _playerVitals.OnCoinsGathered -= OnCoinsGathered;

        _playerVitals = null;
    }

    private void OnCoinsGathered(int amount)
    {
        if (!coinsText) return;
        
        coinsText.SetText(amount.ToString());   
    }
    private void OnHPChanged(int current, int max)
    {
        float percent = (max <= 0) ? 0f : (float)current / max;

        if (healthSlider != null) healthSlider.fillAmount = percent;
        if (healthText != null) healthText.text = current.ToString();
    }

    private void OnXPChanged(float percent01)
    {
        if (xpSlider != null) xpSlider.fillAmount = percent01;
        if (xpText != null) xpText.text = $"{Mathf.RoundToInt(percent01 * 100f)}%";
    }

    private void OnLevelChanged(int level)
    {
        if (levelText != null) levelText.SetText(LevelText + level);
    }

    private void OnDied()
    {
        // Optional: show red flash, disable HUD, etc.
        // (don’t load scene here — player/game systems should do that)
    }
}
