using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollected : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalCoinsText;
    private int _coinsGathered;
    private int _totalCoins;

    // Start is called before the first frame update
    void Start()
    {
        _coinsGathered = PlayerPrefs.GetInt("PlayerCoins" , 0);
        UpdatePlayerCoins();
    }

    private void UpdatePlayerCoins()
    {
        _totalCoins += _coinsGathered;
        totalCoinsText.text = $"Coins: {_totalCoins}";

        // Save to PlayerPrefs to ensure persistence
        PlayerPrefs.SetInt("PlayerCoins", _totalCoins);
        PlayerPrefs.Save();
    }
}
