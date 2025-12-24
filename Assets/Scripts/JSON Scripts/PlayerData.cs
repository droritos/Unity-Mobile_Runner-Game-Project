#if UNITY_EDITOR

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

//Not Used Code
public class PlayerData : MonoSingleton<PlayerData>
{
    public TMP_InputField playerNameInput;
    public TMP_InputField playerAgeInput;
    public WriteListToFile writeListToFile; // Reference to the WriteListToFile script
    public ReadListFromFile readListFromFile; // Reference to the ReadListFromFile script
    public WebcamCapture webCamCapture; // Reference to the WebCamCapture script
    public string LastTimeLogin;
    public int coins;



    public void TakePhoto()
    {
        webCamCapture.CaptureAndSaveImage();
    }

    public void SaveProfile()
    {
        string playerName = playerNameInput.text;
        int playerAge = int.Parse(playerAgeInput.text);
        writeListToFile.SavePlayerData(playerName, playerAge, "capturedImage.png");
        LastTimeLogin = DateTime.Now.Date.ToString();
        this.coins = GameManager.Instance.PlayerManager.PlayerBehavior.CoinsGathered;
    }

    public void LoadProfile()
    {
        List<Player> players = readListFromFile.LoadPlayerData();
        playerNameInput.text = players[0].playerName;
        playerAgeInput.text = players[0].playerAge.ToString();
        GameManager.Instance.PlayerManager.PlayerBehavior.CoinsGathered = this.coins;
    }

}
#endif