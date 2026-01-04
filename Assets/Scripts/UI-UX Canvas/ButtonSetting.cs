using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetting : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] private Image _extraImageLayer;

    private bool _bool = false;

    private void OnValidate()
    {
        if(!_button)
            _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(DoSettingAction);
        DoSettingAction(); // First 
    }

    protected virtual void DoSettingAction()
    {
        _bool = !_bool;                 // new state
        AudioEventManager.ToggleMute(_bool);
        _extraImageLayer.enabled = _bool;
    }

    private void UpdateImage()
    {
        AudioEventManager.ToggleMute(false);
        _extraImageLayer.enabled = false;
    }
}
