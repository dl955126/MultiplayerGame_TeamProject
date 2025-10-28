using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyPool : NetworkBehaviour
{
    [SerializeField] EnemyPathfinding enemy;
    [SerializeField] int enemyAmount;

    Queue<EnemyPathfinding> remainingEnemies = new Queue<EnemyPathfinding>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnNetworkSpawn()
    {
        if(!IsServer) return;

        for(int i = 0; i < enemyAmount; i++)
        {
            var e = Instantiate(enemy);
            e.SetPool(this);
            e.gameObject.SetActive(false);
            
        }
        
    }

    public void SpawnEnemies(Vector3 Location)
    {
        if(!IsServer) return;
        if(remainingEnemies.Count > 0)
        {
            var current = remainingEnemies.Dequeue();
            current.gameObject.SetActive(true);
            current.transform.position = Location;
            print("Enemy spawned from pool");

            current.GetComponent<NetworkObject>().Spawn();
        }
    }

    public void AddToQueue(EnemyPathfinding enemy)
    {
        if (!IsServer) return;
        print("Enemy added to pool");
        remainingEnemies.Enqueue(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
