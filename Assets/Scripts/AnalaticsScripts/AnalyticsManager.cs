using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    private void OnEnable()
    {
        EventManager.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        EventManager.OnGameOver -= OnGameOver;
    }


    private void TriggerGameOverEvent(PlayerArtitube player)
    {
        int level = player.GetLevel();
        CustomEvent gameOver = new CustomEvent("gameOver")
        {
            {"levelDiedOn" , level }
        };
        AnalyticsService.Instance.RecordEvent(gameOver);
        Debug.Log($"Event: gameOver, {level} Recorded");
    }

    private void OnGameOver(PlayerArtitube player)
    {
        TriggerGameOverEvent(player);
    }
}
