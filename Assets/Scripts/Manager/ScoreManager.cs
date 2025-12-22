using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    [Header("Text Mesh Pro")]
    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] TextMeshProUGUI difficultyScaler;

    [Header("Private Data")]
    private PlayerBehavior _playerBehavior;
    public float TotalScore {get; private set;}
    private float _survivedScore;
    private int _coinCollected;
    private float _levelUpBonus = 0;
    
    private const string ScoreConstText = "Score: ";
    

    private void Start()
    {
        _playerBehavior = GameManager.Instance.PlayerManager.PlayerBehavior;
    }

    void Update()
    {
        UpdateScore();
        //UpdateCoins();
    }

    private void UpdateScore()
    {
        SetTimeScore();
        SetCoins();
        ScoreText();
    }

    private void ScoreText() // Do Not Delete Me!!! , I am connection to the enemy spawner
    {
        TotalScore = _coinCollected + _survivedScore + _levelUpBonus; // Getting Updated
        difficultyScaler.SetText(ScoreConstText + Mathf.FloorToInt(TotalScore));
    }

    private void UpdateCoins()
    {
        coins.text = _coinCollected.ToString();
    }

    private void SetTimeScore()
    {
        if (_playerBehavior.playerVitals.IsAlive) // When false , stops adding points to the score
        {
            _survivedScore += Time.deltaTime * 5;
        }
    }

    private void SetCoins()
    {
        _coinCollected = _playerBehavior.CoinsGathered * 10;
    }

    // public float GetScore()
    // {
    //     return Int32.Parse(difficultyScaler.text);
    // }
    public void AddToScore(float gainedScore)
    {
        _levelUpBonus += gainedScore;
    }

    public int GetCoinsCollected()
    {
        return _coinCollected;
    }
}
