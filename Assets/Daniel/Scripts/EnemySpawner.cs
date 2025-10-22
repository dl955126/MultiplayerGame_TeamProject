using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] EnemyPool pool;
    [SerializeField] Transform[] enemySpawnPoints;
    int spawnPointIndex = 0;
    [SerializeField] float spawnRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InvokeRepeating("SpawnEnemy", 2, spawnRate);
    }


    public void SpawnEnemy()
    {
        //Instantiate(enemy, enemySpawnPoints[spawnPointIndex]);
        pool.SpawnEnemies(enemySpawnPoints[spawnPointIndex].position);
        spawnPointIndex++;

        if(spawnPointIndex >= enemySpawnPoints.Length)
        {
            spawnPointIndex = 0;
        }
    }

}
