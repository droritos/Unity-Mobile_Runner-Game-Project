using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewards : MonoBehaviour
{
    private DateTime currentDateTime;
    int[] RewardCoins;

    void Awake()
    {
        currentDateTime = DateTime.Now.Date;
        CheckPlayerLastLogin();
    }

    private void CheckPlayerLastLogin()
    {
        int playerLastDay = int.Parse(PlayerData.Instance.LastTimeLogin);
        if (currentDateTime.Day > playerLastDay)
        {
            Debug.Log($"Last Time Player Logged {playerLastDay}");
        }
    }
}
