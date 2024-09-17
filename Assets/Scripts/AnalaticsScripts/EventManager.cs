using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Action<PlayerArtitube> OnGameOver;

    public static void InvokeGameOver(PlayerArtitube player)
    {
        if (player != null)
        {
            OnGameOver?.Invoke(player);
            Debug.Log("Event : Game Over");
        }
    }
}
