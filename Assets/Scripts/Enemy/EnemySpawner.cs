using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject robotEnemy;

    [SerializeField] Transform leftSpawner;
    [SerializeField] Transform middleSpawner;
    [SerializeField] Transform rightSpawner;

    [SerializeField] RobotEnemyScript enemyScript;

    [SerializeField] int fallingSpeed = 5;


    void Start()
    {
        //DisableSpanwerVisuals();
        
    }

    void Update()
    {
        PoolEnemiesFromSky();
    }

    private void PoolEnemiesFromSky()
    {
        float score = ScoreManager.Instance.GetScore();
        if (score % 15 == 0 && score != 0)
        {
            //GameObject enemy = enemyScript.GetObject();
            //MakeEnemiesFall(enemy);
        }
        Debug.Log($"Score {score}");
    }

    private void DisableSpanwerVisuals()
    {
        leftSpawner.gameObject.SetActive(false);
        middleSpawner.gameObject.SetActive(false);
        rightSpawner.gameObject.SetActive(false);
    }
    private void MakeEnemiesFall(GameObject enemy)
    {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, middleSpawner.position, fallingSpeed * Time.deltaTime);
    }
}
