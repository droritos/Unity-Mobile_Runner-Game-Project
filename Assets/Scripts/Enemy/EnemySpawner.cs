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


    void Start()
    {
        //DisableSpanwerVisuals();
        GetEnemis();
    }

    void Update()
    {
        PoolEnemiesFromSky();
    }

    private void GetEnemis()
    {
        for (int i = 0; i < enemyParent.childCount; i++)
        {
            robotEnemyPool.GetObject();
            //enemyParent.GetChild(i).TryGetComponent(out RobotEnemyScript component);
            //RobotEnemyScript enemy = component;
            //enemiesList.Add(enemy);
            //Debug.Log("Enmey List Is : " + enemiesList.Count);
        }
    }

    private void PoolEnemiesFromSky()
    {
        float score = ScoreManager.Instance.GetScore();
        if (score % 15 == 0 && score != 0)
        {
            enemiesList[0].EnemyPooled();
            //enemyScript.
            //MakeEnemiesFall(enemy);
        }
        //Debug.Log($"Score {score}");
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
