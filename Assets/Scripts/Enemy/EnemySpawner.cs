using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
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
        [SerializeField] float roadYLevel = 0f; 
        [SerializeField] float spawnHeight = 20f; 
        [SerializeField] float startFallSpeed = 10f;
        [SerializeField] float maxFallSpeed = 30f; 

        [Header("Collision Settings")]
        [SerializeField] float spacingCheckRadius = 2.0f; 
        [SerializeField] float spaceBetweenEnemies = 5.0f; 
        [SerializeField] LayerMask avoidanceLayer; 
    
        [Header("References")]
        [SerializeField] Transform[] spawnPoints; 
        [SerializeField] ObjectPoolManager robotEnemyPool; 
    
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
                // --- SAFETY CHECK START ---
                // "Are we waaaay behind?"
                // If the Score is higher than the Threshold + (2 * Interval), we missed multiple waves.
                if (_currentScore > _nextSpawnThreshold + (_currentSpawnInterval * 2))
                {
                    //Debug.Log($"<color=cyan>SCORE JUMP DETECTED!</color> Skipped waves. Score: {_currentScore}, Old Threshold: {_nextSpawnThreshold}");

                    // Option A: Spawn just ONE wave to represent the jump (Recommended)
                    SpawnWave();

                    // Option B: Spawn NOTHING and just reset (If you want to be nice to the player)
                    // (Do nothing here)

                    // Reset the finish line to be ahead of the current score
                    _nextSpawnThreshold = (int)_currentScore + _currentSpawnInterval;
                }
                // --- SAFETY CHECK END ---
                else
                {
                    // Normal behavior: We just crossed the line normally
                    SpawnWave();
                    _nextSpawnThreshold += _currentSpawnInterval;
                }
            }
        }

        private void CalculateDifficulty()
        {
            float effectiveScore = _currentScore * difficultyMultiplier;

            float speedIncrease = effectiveScore / 100f; 
            _currentFallSpeed = Mathf.Clamp(startFallSpeed + speedIncrease, startFallSpeed, maxFallSpeed);

            int intervalDecrease = (int)(effectiveScore / 50f); 
            _currentSpawnInterval = Mathf.Clamp(startSpawnInterval - intervalDecrease, minSpawnInterval, startSpawnInterval);
        }

        private void SpawnWave()
        {
            int countToSpawn = 1;
            if (_currentScore > 1000) countToSpawn = Random.Range(1, maxEnemiesAtOnce + 1);
            else if (_currentScore > 500) countToSpawn = Random.Range(1, 3);

            for (int i = 0; i < countToSpawn; i++)
            {
                SpawnSingleEnemy(i); 
            }
        }

        private void SpawnSingleEnemy(int offsetIndex)
        {
            GameObject enemyObj = robotEnemyPool.GetObject();
            Transform chosenLane = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
            Vector3 groundTargetPos = chosenLane.position;
            groundTargetPos.z += (offsetIndex * spaceBetweenEnemies);

            int safetyCounter = 0;
            while (Physics.CheckSphere(groundTargetPos, spacingCheckRadius, avoidanceLayer) && safetyCounter < 10)
            {
                groundTargetPos.z += spaceBetweenEnemies;
                safetyCounter++;
            }

            Vector3 skySpawnPos = groundTargetPos;
            skySpawnPos.y = spawnHeight; 

            enemyObj.transform.position = skySpawnPos;
            enemyObj.SetActive(true);

            if (enemyObj.TryGetComponent(out EnemyDropBehaviour dropper))
            {
                dropper.StartDrop(groundTargetPos.y, _currentFallSpeed);
            }
        }

        public void SetPaused(bool paused)
        {
            _isPaused = paused;

            // LOGGING: Check when this function is actually called
            //Debug.Log($"SetPaused called with value: {paused}");

            if (!_isPaused) // We are UN-PAUSING
            {
                float realScore = ScoreManager.Instance.TotalScore;

                //Debug.Log($"<color=yellow>UNPAUSE CHECK:</color> Real Score: {realScore} | Current Threshold: {_nextSpawnThreshold}");

                if (realScore >= _nextSpawnThreshold)
                {
                    // FORCE UPDATE DIFFICULTY NOW (Crucial Fix: Difficulty might be stale!)
                    _currentScore = realScore;
                    CalculateDifficulty();

                    int oldThreshold = _nextSpawnThreshold;
                    _nextSpawnThreshold = (int)realScore + _currentSpawnInterval;
            
                    //Debug.Log($"<color=green>FIX APPLIED:</color> Jumped threshold from {oldThreshold} to {_nextSpawnThreshold}");
                }
               
            }
        }
    }
}