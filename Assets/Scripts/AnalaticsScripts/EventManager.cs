using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Action<PlayerVitals> OnGameOver;
    public static Action OnMove;
    public static Action OnTakeDamage;
    public static Action OnPlayerDeath;


    public static void InvokeGameOver(PlayerVitals player)
    {
        if (player != null)
        {
            OnGameOver?.Invoke(player);
            //Debug.Log("Event : Game Over");
        }
    }

    public static void RaiseMove() => OnMove?.Invoke();
    public static void RaiseTakeDamage() => OnTakeDamage?.Invoke();
    
    public static void RaisePlayerDeath() => OnPlayerDeath?.Invoke();

}
