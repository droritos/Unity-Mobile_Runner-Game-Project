using UnityEngine;
using UnityEngine.UI;

public class ButtonSetting : MonoBehaviour
{
    private const string SOUND_PREF_KEY = "SoundMuted";

    [SerializeField] private Button _button;
    [SerializeField] private Image _extraImageLayer;

    private bool _isMuted;

    private void OnValidate()
    {
        if (!_button)
            _button = GetComponent<Button>();
    }

    private void Start()
    {
        LoadPref();
        ApplyState();

        _button.onClick.AddListener(ToggleSound);
    }

    private void ToggleSound()
    {
        _isMuted = !_isMuted;
        SavePref();
        ApplyState();
    }

    private void ApplyState()
    {
        AudioEventManager.ToggleMute(_isMuted);
        _extraImageLayer.enabled = _isMuted;
    }

    private void SavePref()
    {
        PlayerPrefs.SetInt(SOUND_PREF_KEY, _isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadPref()
    {
        _isMuted = PlayerPrefs.GetInt(SOUND_PREF_KEY, 0) == 1;
    }
}