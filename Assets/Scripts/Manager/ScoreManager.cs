using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Text Mesh Pro")]
    [SerializeField] TextMeshProUGUI timeScore;
    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] TextMeshProUGUI score;

    [Header("Serialize Data")]
    [SerializeField] PlayerBehavior playerBehavior;
    [SerializeField] MovingObjectsConfig roadParametersConfig;

    [Header("Private Data")]
    private float _totalScore = 0;
    private float _survivedScore;
    private int _coinScore;


    void Update()
    {
        UpdateScore();
        UpdateCoins();
    }

    private void UpdateScore()
    {
        SetTimeScore();
        SetCoins();
        _totalScore = _coinScore + _survivedScore;
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

}
