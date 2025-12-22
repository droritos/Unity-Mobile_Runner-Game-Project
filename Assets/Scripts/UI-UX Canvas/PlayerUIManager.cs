using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Experience")]
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI levelText;
    
    [Header("Coins")]
    [SerializeField] private TextMeshProUGUI coinsText;

    private PlayerVitals _playerVitals;

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

    // Call this from GameManager when player spawns / is ready
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

  
    public void Unbind()
    {
        if (_playerVitals == null) return;

        _playerVitals.HPChanged -= OnHPChanged;
        _playerVitals.XPPercentChanged -= OnXPChanged;
        _playerVitals.LevelChanged -= OnLevelChanged;
        _playerVitals.Died -= OnDied;
        _playerVitals.OnCoinsGathered -= OnCoinsGathered;

        _playerVitals = null;
    }

    private void OnDestroy() => Unbind();
    private void OnCoinsGathered(int anount) => coinsText.SetText(anount.ToString());   
    private void OnHPChanged(int current, int max)
    {
        float percent = (max <= 0) ? 0f : (float)current / max;

        if (healthSlider != null) healthSlider.value = percent;
        if (healthText != null) healthText.text = current.ToString();
    }

    private void OnXPChanged(float percent01)
    {
        if (xpSlider != null) xpSlider.value = percent01;
        if (xpText != null) xpText.text = $"{Mathf.RoundToInt(percent01 * 100f)}%";
    }

    private void OnLevelChanged(int level)
    {
        if (levelText != null) levelText.text = level.ToString();
    }

    private void OnDied()
    {
        // Optional: show red flash, disable HUD, etc.
        // (don’t load scene here — player/game systems should do that)
    }
}
