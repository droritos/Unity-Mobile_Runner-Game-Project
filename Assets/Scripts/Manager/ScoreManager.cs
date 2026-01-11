using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager> , IPausable
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
    private bool _isPaused;

    private const string ScoreConstText = "Score: ";
    private int _lastDisplayedScore = -1;
    

    private void Start()
    {
        _playerBehavior = GameManager.Instance.PlayerManager.PlayerBehavior;
        PauseManager.Instance.Register(this);
    }

    void OnDestroy() => PauseManager.Instance.Unregister(this);

    void Update()
    {
        if(_isPaused) return;
        UpdateScore();
    }

    private void UpdateScore()
    {
        SetTimeScore();
        SetCoins();
        
        TotalScore = _coinCollected + _survivedScore + _levelUpBonus; 
        
        ScoreText(); 
    }

    private void ScoreText() 
    {
        // 1. Convert to int
        int currentScoreInt = Mathf.FloorToInt(TotalScore);

        // 2. CHECK: Did the number actually change?
        if (currentScoreInt != _lastDisplayedScore)
        {
            // 3. Only update the text if it's a new number
            _lastDisplayedScore = currentScoreInt;
            
            // Optimization: Use {0} format to avoid string garbage creation
            difficultyScaler.SetText("Score: {0}", currentScoreInt);
        }
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

    public void SetPaused(bool paused)
    {
        _isPaused =  paused;
    }
}
