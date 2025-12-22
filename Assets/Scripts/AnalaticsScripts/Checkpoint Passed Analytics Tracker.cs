using System;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class CheckpointPassedTracker : MonoBehaviour
{
    public string eventName = "checkpointPassed";

    public PlayerVitals playerVitals;

    private int _newCheckpoint;

    async void Start()
    {
        try
        {
            var options = new InitializationOptions();
            await UnityServices.InitializeAsync(options);
            Debug.Log("Unity Services Initialized successfully.");

            // Retrieve the passing of a new checkpoint which is triggered by a level up
            _newCheckpoint = playerVitals.Level + 1;

            TrackCheckPointPassed();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unity Services initialization failed: {ex.Message}");
        }
    }

    private void TrackCheckPointPassed()
    {
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            try
            {
                var eventParams = new Dictionary<string, object>
                {
                    { "checkpointPassed", _newCheckpoint }
                };

                AnalyticsService.Instance.RecordEvent(eventName);
                AnalyticsService.Instance.Flush();
                Debug.Log($"Analytics event '{eventName}' sent with checkpoint: {_newCheckpoint}");
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
