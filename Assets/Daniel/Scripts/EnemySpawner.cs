using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
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
        Instantiate(enemy, enemySpawnPoints[spawnPointIndex]);
        spawnPointIndex++;

        if(spawnPointIndex >= enemySpawnPoints.Length)
        {
            spawnPointIndex = 0;
        }
    }

}
