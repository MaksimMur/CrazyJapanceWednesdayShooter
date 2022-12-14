using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] posForSpawn;
    [SerializeField] private float timeToSpawn = 4;
    [SerializeField] private int limitOfEnemies=3;
    private int currentAmountEnemies = 0;

    public void SpawnEnemyStart()
    {
        StartCoroutine("SpawnEnemy");
    }
    public void SpawnEnemyStop()
    {
        StopCoroutine("SpawnEnemy");
    }
    public IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (currentAmountEnemies < limitOfEnemies)
            {
                int randomEnemy = Random.Range(0, enemies.Length);
                int randomPos = Random.Range(0, posForSpawn.Length);
                GameObject go = Instantiate(enemies[randomEnemy]);
                go.transform.position = posForSpawn[randomPos].position;
                currentAmountEnemies++;
            }
            yield return new WaitForSeconds(timeToSpawn);
        }

    }
}
