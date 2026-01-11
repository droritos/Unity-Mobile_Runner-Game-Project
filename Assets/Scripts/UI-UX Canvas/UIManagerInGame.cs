using System;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using UnityEngine.Events;

public class UIManagerInGame : MonoBehaviour
{
    [SerializeField] UpgradeMenu upgradeMenu;
    [SerializeField] DamageFeedback damageFeedback;

    private void Start()
    {
        EventManager.OnTakeDamage += FlashFeedback;
    }

    private void OnDestroy()
    {
        EventManager.OnTakeDamage -= FlashFeedback;
    }

    private void FlashFeedback()
    {
        damageFeedback.TriggerDamageFlash();
    }
}
