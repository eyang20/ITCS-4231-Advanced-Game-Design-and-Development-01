using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    EnemySpawnPooling enemySpawnPooling;
    public int enemyCount = 0;

    private void Start()
    {
        //enemySpawnPooling is the Script <EnemySpawnPooling>()
        enemySpawnPooling = EnemySpawnPooling.Instance;
        
    }
    void Update()
    {
        //If the value enemyCount from the script <EnemySpawnPooling>() is less the value enemyCount in this script
        if (enemySpawnPooling.enemyCount > enemyCount)
        {
            //Calls SpawnFromPool from the script <EnemySpawnPooling>().
            enemySpawnPooling.SpawnFromPool("Basic_Enemy", transform.position, Quaternion.identity);
            enemyCount++;
        }

    }
}
