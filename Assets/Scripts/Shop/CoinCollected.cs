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
    public bool TryBuyUpgrade(int cost)
    {
        int currentCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        if (currentCoins >= cost)
        {
            DeductCoins(cost);
            return true;
        }
        else
        {
            WarninningMoney();
            Debug.Log("Not Enough Money");
            return false;
        }
    }

    private void UpdatePlayerCoins()
    {
        _totalCoins += _coinsGathered;
        totalCoinsText.text = $"{_totalCoins}";

        // Save to PlayerPrefs to ensure persistence
        PlayerPrefs.SetInt("PlayerCoins", _totalCoins);
        PlayerPrefs.Save();
    }

    private void DeductCoins(int cost)
    {
        int currentCoins = PlayerPrefs.GetInt("PlayerCoins", 0);

        // Deduct the cost
        currentCoins -= cost;

        // Update PlayerPrefs with the new coin value
        PlayerPrefs.SetInt("PlayerCoins", currentCoins);
        PlayerPrefs.Save();

        // Update the UI to reflect the new coin balance
        UpdateCoinUI();
    }

    // Update the coin display on the UI
    private void UpdateCoinUI()
    {
        int currentCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        totalCoinsText.text = currentCoins.ToString(); // Update the UI element showing the coins
    }

    private void WarninningMoney()
    {
        // Start the coroutine to flash the text
        StartCoroutine(FlashTextRed());
    }

    // Coroutine to flash the text color to red and back to the original color
    private IEnumerator FlashTextRed()
    {
        Color originalColor = totalCoinsText.color; // Store the original color of the text
        Color warningColor = Color.red; // Set the warning color to red

        int flashCount = 3; // Number of times to flash
        float flashDuration = 0.3f; // Duration of each flash

        for (int i = 0; i < flashCount; i++)
        {
            // Change the color to red
            totalCoinsText.color = warningColor;
            yield return new WaitForSeconds(flashDuration);

            // Change the color back to the original
            totalCoinsText.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
