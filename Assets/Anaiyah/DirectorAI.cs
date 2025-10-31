using UnityEngine;
using Unity.Netcode;
using Anaiyah;

public class DirectorAI : NetworkBehaviour
{
    [Header("References")] 
    public PlayerHealth _player;
    public EnemySpawner _spawner;

    [Header("SpawnInerval")] 
    public float minSpawnInterval;
    public float maxSpawnInterval;
    public float currentSpawnInterval;

    [Header("Game State")] 
    public float checkState = 2f;
    public float fullHealth = 100f;

    private float checkTimer;

    bool hasFoundPlayer = false;
    bool hasFoundSpawner = true;


    void Update()
    {
        if (!IsServer) return;

        if (!hasFoundPlayer)
        {
            findPlayer();
        }
        if(!hasFoundSpawner)
        {
            findSpawner();
        }

        if (_spawner != null && _spawner.gameHasStarted.Value)
        {
            checkTimer += Time.deltaTime;

            if (checkTimer >= checkState)
            {
                checkTimer = 0;
                EvaluateState();
            }
        }
    }

    public void findPlayer()
    {
        _player = FindAnyObjectByType<PlayerHealth>();
        hasFoundPlayer = true;
    }

    public void findSpawner()
    {
        _spawner = FindAnyObjectByType<EnemySpawner>();
        hasFoundSpawner = true;
    }

    void EvaluateState()
    {
        if (_player == null)
            return;
        if (_spawner == null)
            return;
        
        float playerHealth = _player.currentHealth;

        //if player is half health increase spawntimer

        if (playerHealth >= 50)
        {
            if (currentSpawnInterval > minSpawnInterval)
                currentSpawnInterval -= 0.5f;
        }
        else
        {
            if (currentSpawnInterval < maxSpawnInterval)
                currentSpawnInterval += 0.5f;
        }
    }

}
