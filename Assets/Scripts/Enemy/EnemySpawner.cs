using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField] GameObject robotEnemy;
    [SerializeField] int fallingSpeed = 5;

    [Header("Spawn Points")]
    [SerializeField] Transform leftSpawner;
    [SerializeField] Transform middleSpawner;
    [SerializeField] Transform rightSpawner;

    [Header("Enemy Belongings")]
    [SerializeField] List<RobotEnemyScript> enemiesList;
    [SerializeField] Transform enemyParent;
    [SerializeField] ObjectPoolManager robotEnemyPool;

    private bool _wasPooled = false;
    private float _score;
    void Update()
    {
        TryPoolEnemies();
    }
    private void TryPoolEnemies()
    {
        float score = ScoreManager.Instance.GetScore();
        if (score % 15 == 0 && score != 0 && !_wasPooled)
        {
            GameObject enemy = robotEnemyPool.GetObject();
            if (enemy.TryGetComponent(out RobotEnemyScript component))
            {
                enemiesList.Add(component);
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
        else if (score % 15 != 0 && _wasPooled)
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
        foreach (RobotEnemyScript enemy in enemiesList)
        {
            if (enemy.gameObject.activeInHierarchy && Vector3.Distance(enemy.transform.position, position) < 1.0f)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator LerpEnemyPosition(GameObject enemy, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float journeyTime = 2f;  // Adjust this value for the speed of movement
        Vector3 startPosition = enemy.transform.position;

        while (elapsedTime < journeyTime)
        {
            enemy.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / journeyTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is exactly at the target position
        enemy.transform.position = targetPosition;
    }
}
