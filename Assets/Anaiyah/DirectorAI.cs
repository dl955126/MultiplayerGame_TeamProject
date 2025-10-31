using UnityEngine;
using Unity.Netcode;
using Anaiyah;

public class DirectorAI : NetworkBehaviour
{
    [Header("References")] 
    public PlayerHealth _player;
    public EnemySpawner _spawner;

    [Header("SpawnInerval")] public float minSpawnInterval;
    public float maxSpawnInterval;
    private float currentSpawnInterval;

    [Header("Game State")] 
    public float checkState = 2f;
    public float fullHealth = 100f;

    private float checkTimer;

    void Start()
    {
        if (_spawner != null)
        {
            //_spawner.spawnRate = currentSpawnInterval;
        }
    }

    void Update()
    {
        if (!IsServer) return;
       checkTimer += Time.deltaTime;

       if (checkTimer >= checkState);
       {
           checkTimer = 0;
           EvaluateState();
       }
    }

    void EvaluateState()
    {
        if (_player == null || _spawner = null)
            return;
        
        float playerHealth = _player.currentHealth;

        if (playerHealth == fullHealth)
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

    void UpdateSpawnRate(float newSpawnRate)
    {
        _spawner.spawnRate = newSpawnRate;
    }


}
