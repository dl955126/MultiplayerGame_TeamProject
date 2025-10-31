using System.Collections;
using Unity.Netcode;
using UnityEditor.Search.Providers;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] EnemyPool pool;
    [SerializeField] Transform[] enemySpawnPoints;
    int spawnPointIndex = 0;
    [SerializeField] DirectorAI directorAI;
    public NetworkVariable<bool> gameHasStarted = new NetworkVariable<bool>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnNetworkSpawn()
    {
        if(!IsServer) return;
        gameHasStarted.Value = false;
    }

    public void OnStartButton()
    {
        if(!IsOwner) return; //only owner can start the game
        StartGameRpc();
    }

    [Rpc(SendTo.Server)]
    public void StartGameRpc()
    {
        gameHasStarted.Value = true;
        StartCoroutine(SpawnLoop());
        //InvokeRepeating("SpawnEnemy", 2, directorAI.currentSpawnInterval);
        Debug.Log("GAME HAS STARTED");
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {

            SpawnEnemy() ;

            float spawnInterval = directorAI.currentSpawnInterval;
            yield return new WaitForSeconds(spawnInterval);
        }

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
