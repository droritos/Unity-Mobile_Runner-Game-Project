using System;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class CoinsCollectedAnalyticsTracker : MonoBehaviour
{
    public string eventName = "collectItem";

    private int _coinsCollected;

    public CoinCollected coinCollected;

    async void Start()
    {
        try
        {
            var options = new InitializationOptions();
            await UnityServices.InitializeAsync(options);
            Debug.Log("Unity Services Initialized successfully.");

            _coinsCollected = coinCollected._coinsGathered;

            TrackCoinsCollected();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unity Services initialization failed: {ex.Message}");
        }
    }

    private void TrackCoinsCollected()
    {
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            try
            {
                var eventParams = new Dictionary<string, object>
                {
                    { "collectItem", _coinsCollected }
                };

                AnalyticsService.Instance.RecordEvent(eventName);
                AnalyticsService.Instance.Flush();
                Debug.Log($"Analytics event '{eventName}' sent with coins: {_coinsCollected}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to send analytics event: {ex.Message}");
            }
        }
        else
        {
            Debug.LogError("Unity Services are not initialized, cannot send analytics event.");
        }
    }
}
