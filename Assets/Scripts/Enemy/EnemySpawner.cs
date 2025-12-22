using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField] int fallingSpeed = 5;

    [Header("Spawn Points")]
    [SerializeField] Transform leftSpawner;
    [SerializeField] Transform middleSpawner;
    [SerializeField] Transform rightSpawner;

    [Header("Enemy Belongings")]
    public List<RobotEnemyScript> EnemiesList;
    public Transform EnemyParent;
    [SerializeField] ObjectPoolManager robotEnemyPool;

    [Header("Spawner Data")]
    [SerializeField] int scoreToSpawn = 25;


    private bool _wasPooled = false;
    private int _score;
    void Update()
    {
        TryPoolEnemies();
    }
    private void TryPoolEnemies()
    {
        _score = Mathf.FloorToInt(ScoreManager.Instance.TotalScore);
        if (_score % scoreToSpawn == 0 && _score != 0 && !_wasPooled)
        {
            GameObject enemy = robotEnemyPool.GetObject();
            if (enemy.TryGetComponent(out RobotEnemyScript component))
            {
                EnemiesList.Add(component);
            }

            Transform chosenSpawnPoint = GetRandomSpawnPoint();
            Vector3 enemyPosition = chosenSpawnPoint.position;

            // Check if there's already an enemy at the chosen position and keep moving along Z-axis if occupied
            while (IsEnemyAtPosition(enemyPosition))
            {
                enemyPosition = new Vector3(enemyPosition.x, enemyPosition.y, enemyPosition.z + 3);
            }

            StartCoroutine(LerpEnemyPosition(enemy, enemyPosition));
            _wasPooled = true;
        }
        else if (_score % scoreToSpawn != 0 && _wasPooled)
        {
            // Reset the flag when the score is no longer a multiple of 15
            _wasPooled = false;
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, 2);  // Assuming you have three spawn points: Left, Middle, Right
        switch (randomIndex)
        {
            case 0:
                return leftSpawner;
            case 1:
                return middleSpawner;
            case 2:
                return rightSpawner;
            default:
                return middleSpawner;  // Fallback to middle spawner if something goes wrong
        }
    }

    private bool IsEnemyAtPosition(Vector3 position)
    {
        for (int i = EnemiesList.Count - 1; i >= 0; i--)
        {
            RobotEnemyScript enemy = EnemiesList[i];

            // Remove inactive enemies
            if (!enemy.gameObject.activeInHierarchy)
            {
                EnemiesList.RemoveAt(i);
                continue;
            }

            // Check if the enemy is at the given position
            if (Vector3.Distance(enemy.transform.position, position) < 1.0f)
            {
                return true; // An enemy is found at the position
            }
        }

        return false; // No enemy at this position
    }

        private void SetDiffculty()
    {
        if (_score >= 50 && _score < 100)
        {
            scoreToSpawn = 30;
            fallingSpeed = 4;
        }
        else if (_score >= 100 && _score < 150)
        {
            scoreToSpawn = 25;
            fallingSpeed = 5;
        }
        else if (_score >= 150 && _score < 200)
        {
            scoreToSpawn = 20;
            fallingSpeed = 6;
        }
        else if (_score >= 200 && _score < 300)
        {
            scoreToSpawn = 15;
            fallingSpeed = 7;
        }
        else if (_score >= 300 && _score < 400)
        {
            scoreToSpawn = 12;
            fallingSpeed = 8;
        }
        else if (_score >= 400 && _score < 500)
        {
            scoreToSpawn = 10;
            fallingSpeed = 9;
        }
        else if (_score >= 500 && _score < 600)
        {
            scoreToSpawn = 8;
            fallingSpeed = 10;
        }
        else if (_score >= 600 && _score < 800)
        {
            scoreToSpawn = 7;
            fallingSpeed = 11;
        }
        else if (_score >= 800 && _score < 1000)
        {
            scoreToSpawn = 6;
            fallingSpeed = 12;
        }
        else if (_score >= 1000)
        {
            scoreToSpawn = 5; // Maximum spawn rate
            fallingSpeed = 14; // Maximum falling speed
        }
    }

    private IEnumerator LerpEnemyPosition(GameObject enemy, Vector3 targetPosition)
    {
        float distance = Vector3.Distance(enemy.transform.position, targetPosition);
        float elapsedTime = 0f;
        Vector3 startPosition = enemy.transform.position;

        while (elapsedTime < distance / fallingSpeed)
        {
            enemy.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / (distance / fallingSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is exactly at the target position
        enemy.transform.position = targetPosition;
    }
}
