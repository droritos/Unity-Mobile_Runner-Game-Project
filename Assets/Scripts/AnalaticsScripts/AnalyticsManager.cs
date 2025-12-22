using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine.UnityConsent;

public class AnalyticsManager : MonoBehaviour
{
    private bool _initialized;
    private bool _analyticsAllowed;

    private async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();

            // TODO: replace with saved choice / popup result
            _analyticsAllowed = true;

            EndUserConsent.SetConsentState(new ConsentState
            {
                AnalyticsIntent = _analyticsAllowed ? ConsentStatus.Granted : ConsentStatus.Denied
            });

            _initialized = true;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            _initialized = false;
        }
    }

    private void OnEnable() => EventManager.OnGameOver += OnGameOver;
    private void OnDisable() => EventManager.OnGameOver -= OnGameOver;

    private void OnGameOver(PlayerVitals player)
    {
        if (!_initialized || !_analyticsAllowed) return;

        var gameOver = new CustomEvent("gameOver")
        {
            { "levelDiedOn", player.Level }
        };

        AnalyticsService.Instance.RecordEvent(gameOver);
        Debug.Log($"Event: gameOver, {player.Level} Recorded");
    }
}