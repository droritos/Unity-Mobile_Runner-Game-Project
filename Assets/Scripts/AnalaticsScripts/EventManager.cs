using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Action<PlayerVitals> OnGameOver;

    public static void InvokeGameOver(PlayerVitals player)
    {
        if (player != null)
        {
            OnGameOver?.Invoke(player);
            Debug.Log("Event : Game Over");
        }
    }
}
