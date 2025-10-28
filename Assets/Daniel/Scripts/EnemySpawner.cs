using Unity.Netcode;
using UnityEditor.Search.Providers;
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

    public void OnStartButton()
    {
        if(!IsOwner) return; //only owner can start the game
        StartGameRpc();
    }

    [Rpc(SendTo.Server)]
    public void StartGameRpc()
    {
        InvokeRepeating("SpawnEnemy", 2, spawnRate);
        Debug.Log("GAME HAS STARTED");
    }


    public void SpawnEnemy()
    {
        if(!IsServer) return; //only server spawns enemies

        //Instantiate(enemy, enemySpawnPoints[spawnPointIndex]);
        pool.SpawnEnemies(enemySpawnPoints[spawnPointIndex].position);
        spawnPointIndex++;

        if(spawnPointIndex >= enemySpawnPoints.Length)
        {
            spawnPointIndex = 0;
        }
    }

}
