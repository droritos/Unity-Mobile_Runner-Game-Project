using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour, IPausable
{
    [Header("Difficulty Settings")]
    [Tooltip("Higher = Harder faster")]
    [SerializeField] float difficultyMultiplier = 1.0f; 
    [SerializeField] int startSpawnInterval = 25; 
    [SerializeField] int minSpawnInterval = 5;

    [Header("Horde Settings")]
    [SerializeField] int maxEnemiesAtOnce = 3; 

    [Header("Drop Settings")]
    [SerializeField] float roadYLevel = 0f; // Where is the ground?
    [SerializeField] float startFallSpeed = 10f;
    [SerializeField] float maxFallSpeed = 30f; // Falls faster as game gets harder

    [Header("References")]
    [SerializeField] Transform[] spawnPoints; // Left, Middle, Right
    [SerializeField] ObjectPoolManager robotEnemyPool; // Your existing pool
    
    // Internal State
    private float _currentScore;
    private int _nextSpawnThreshold;
    private float _currentFallSpeed;
    private int _currentSpawnInterval;
    private bool _isPaused;

    void Start()
    {
        PauseManager.Instance.Register(this);
        
        // Init Defaults
        _currentFallSpeed = startFallSpeed;
        _currentSpawnInterval = startSpawnInterval;
        _nextSpawnThreshold = startSpawnInterval;
    }

    private void OnDestroy() => PauseManager.Instance.Unregister(this);

    void Update()
    {
        if (_isPaused) return;

        // 1. Difficulty Math
        _currentScore = ScoreManager.Instance.TotalScore;
        CalculateDifficulty();

        // 2. Spawn Check
        if (_currentScore >= _nextSpawnThreshold)
        {
            SpawnWave();
            _nextSpawnThreshold += _currentSpawnInterval;
        }
    }

    private void CalculateDifficulty()
    {
        float effectiveScore = _currentScore * difficultyMultiplier;

        // Fall Speed increases
        float speedIncrease = effectiveScore / 100f; 
        _currentFallSpeed = Mathf.Clamp(startFallSpeed + speedIncrease, startFallSpeed, maxFallSpeed);

        // Interval Decreases (Spawns faster)
        int intervalDecrease = (int)(effectiveScore / 50f); 
        _currentSpawnInterval = Mathf.Clamp(startSpawnInterval - intervalDecrease, minSpawnInterval, startSpawnInterval);
    }

    private void SpawnWave()
    {
        // Calculate how many to spawn (1 -> 2 -> 3 based on score)
        int countToSpawn = 1;
        if (_currentScore > 1000) countToSpawn = Random.Range(1, maxEnemiesAtOnce + 1);
        else if (_currentScore > 500) countToSpawn = Random.Range(1, 3);

        for (int i = 0; i < countToSpawn; i++)
        {
            SpawnSingleEnemy(i); // Pass index to help offset positions
        }
    }

    private void SpawnSingleEnemy(int offsetIndex)
    {
        GameObject enemyObj = robotEnemyPool.GetObject();
        
        // 1. Position Logic
        Transform chosenLane = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 spawnPos = chosenLane.position;

        // OFFSET: If spawning a "Horde" (multiple at once), push the later ones back
        // so they don't land inside each other.
        spawnPos.z += (offsetIndex * 4.0f); 

        // 2. Set Position & Active
        enemyObj.transform.position = spawnPos;
        enemyObj.SetActive(true); // Ensure pool activates it

        // 3. Trigger the Fall (Using the NEW script)
        if (enemyObj.TryGetComponent(out EnemyDropBehaviour dropper))
        {
            dropper.StartDrop(roadYLevel, _currentFallSpeed);
        }
        else
        {
            Debug.LogWarning("Enemy is missing 'EnemyDropBehaviour' script!");
        }
        
        // Note: Your RobotEnemyScript runs its own Start/Update automatically
    }

    public void SetPaused(bool paused) => _isPaused = paused;
}