using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject> enemiesInPlay = new List<GameObject>();
    public List<Transform> spawnPoints = new List<Transform>();

    private int waveNumber = 1;
    private bool spawning = false;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        RemoveObjectFromList();
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            if (enemiesInPlay.Count == 0 && !spawning)
            {
                spawning = true;
                yield return StartCoroutine(SpawnWave(waveNumber));
                waveNumber++;
                spawning = false;
            }

            yield return null;
        }
    }

    IEnumerator SpawnWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount + 4; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnTransform = spawnPoints[randomIndex];

        GameObject spawnedEnemy = Instantiate(enemy, spawnTransform.position, Quaternion.identity);
        AddObjectToList(spawnedEnemy);
    }

    void AddObjectToList(GameObject spawnedObject)
    {
        enemiesInPlay.Add(spawnedObject);
    }

    void RemoveObjectFromList()
    {
        for (int i = enemiesInPlay.Count - 1; i >= 0; i--)
        {
            if (enemiesInPlay[i] == null || enemiesInPlay[i].GetComponent<CharacterStats>().health <= 0)
            {
                enemiesInPlay.RemoveAt(i);
            }
        }
    }
}
