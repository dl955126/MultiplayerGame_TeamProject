using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] EnemyPool pool;
    [SerializeField] Transform[] enemySpawnPoints;
    int spawnPointIndex = 0;
    [SerializeField] float spawnRate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    public void OnStartButtonPress()
    {
        if (IsServer)
        {
            StartGame();
        }
        else
        {
            StartGameRpc();
        }
    }

    public void StartGame()
    {
        if (!IsServer) return;

        InvokeRepeating("SpawnEnemy", 2, spawnRate);
    }

    [Rpc(SendTo.Server)]
    public void StartGameRpc()
    {
        StartGame();
    }


    public void SpawnEnemy()
    {

        if (!IsServer) return;

        //Instantiate(enemy, enemySpawnPoints[spawnPointIndex]);
        pool.SpawnEnemies(enemySpawnPoints[spawnPointIndex].position);
        spawnPointIndex++;

        if(spawnPointIndex >= enemySpawnPoints.Length)
        {
            spawnPointIndex = 0;
        }
    }

}
