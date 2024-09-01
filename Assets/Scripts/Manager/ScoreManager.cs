using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    [Header("Text Mesh Pro")]
    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] TextMeshProUGUI score;

    [Header("Serialize Data")]
    [SerializeField] PlayerBehavior playerBehavior;

    [Header("Private Data")]
    private float _totalScore = 0;
    private float _survivedScore;
    private int _coinScore;
    private float _levelUpBonus = 0;


    void Update()
    {
        UpdateScore();
        UpdateCoins();
    }

    private void UpdateScore()
    {
        SetTimeScore();
        SetCoins();
        ScoreText();
    }

    private void ScoreText()
    {
        _totalScore = _coinScore + _survivedScore + _levelUpBonus;
        score.text = Mathf.FloorToInt(_totalScore).ToString();
    }

    private void UpdateCoins()
    {
        coins.text = _coinScore.ToString();
    }

    private void SetTimeScore()
    {
        if (playerBehavior.IsAlive)
        {
            _survivedScore += Time.deltaTime * 5;
        }
    }

    private void SetCoins()
    {
        _coinScore = playerBehavior.coins * 10;
    }

    public float GetScore()
    {
        return Int32.Parse(score.text);
    }
    public void AddToScore(float gainedScore)
    {
        _levelUpBonus += gainedScore;
    }
}
