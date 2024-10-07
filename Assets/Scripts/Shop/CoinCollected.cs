using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollected : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalCoinsText;
    [SerializeField] TextMeshProUGUI coinsGatheredText;
    [SerializeField] Button addCoinsSecretButton;
    public int CoinsGathered;

    private int _totalCoins;

    // Start is called before the first frame update
    void Start()
    {
        CoinsGathered = PlayerPrefs.GetInt("PlayerCoins" , 0);
        UpdatePlayerCoins();

        if(coinsGatheredText != null)
            coinsGatheredText.text = PlayerPrefs.GetInt("CoinsFromThisSessin", 0).ToString();

        if (addCoinsSecretButton != null)
            SetSecretButton();

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
            WarninningMoneyAnimation();
            Debug.Log("Not Enough Money");
            return false;
        }
    }
    public void SetSecretButton()
    {
        // Buttons to add currency coins for check the ShopHandler is working
        addCoinsSecretButton.onClick.RemoveAllListeners();
        addCoinsSecretButton.onClick.AddListener(UpdatePlayerCoins);
    }

    private void UpdatePlayerCoins()
    {
        _totalCoins += CoinsGathered;
        if(totalCoinsText != null)
            totalCoinsText.text = $"{_totalCoins}";

        // Save to PlayerPrefs to ensure persistence
        PlayerPrefs.SetInt("PlayerCoins", _totalCoins);
        PlayerPrefs.Save();
        //Debug.Log($"Total : {_totalCoins}, Gathered {_coinsGathered}");
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

    private void WarninningMoneyAnimation()
    {
        // Start the coroutine to flash the text
        StartCoroutine(FlashTextRed());
    }

    // Coroutine to flash the text color to red and back to the original color
    private IEnumerator FlashTextRed()
    {
        Color originalColor = totalCoinsText.color; // Store the original color of the text
        Color warningColor = Color.red; // Set the warning color to red

        float flashDuration = 0.3f; // Duration of each flash

            // Change the color to red
            totalCoinsText.color = warningColor;
            yield return new WaitForSeconds(flashDuration);

            // Change the color back to the original
            totalCoinsText.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
    }
}
