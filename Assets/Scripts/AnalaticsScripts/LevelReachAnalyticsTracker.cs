using System;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System.Collections.Generic;

public class LevelReachAnalyticsTracker : MonoBehaviour
{
    public string eventName = "levelGameover"; // Default event name

    private int _reachedLevel;

    async void Start()
    {
        // Initialize Unity Services
        try
        {
            var options = new InitializationOptions();
            await UnityServices.InitializeAsync(options);
            Debug.Log("Unity Services Initialized successfully.");

            // Retrieve the level reached by the player
            _reachedLevel = GameManager.Instance.Player.LevelReachWhenDied();

            // Now that services are initialized, track the level reached
            TrackLevelReached();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unity Services initialization failed: {ex.Message}");
        }
    }

    private void TrackLevelReached()
    {
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            try
            {
                var eventParams = new Dictionary<string, object>
                {
                    { "levelDiedOn", _reachedLevel }
                };

                AnalyticsService.Instance.RecordEvent(eventName);
                AnalyticsService.Instance.Flush(); // Ensure the data is sent immediately
                Debug.Log($"Analytics event '{eventName}' sent with level: {_reachedLevel}");
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
