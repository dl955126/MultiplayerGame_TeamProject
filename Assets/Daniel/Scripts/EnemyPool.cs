using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] EnemyPathfinding enemy;
    [SerializeField] int enemyAmount;

    Queue<EnemyPathfinding> remainingEnemies = new Queue<EnemyPathfinding>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < enemyAmount; i++)
        {
            var e = Instantiate(enemy);
            e.SetPool(this);
            e.gameObject.SetActive(false);

            if (NetworkManager.Singleton.IsServer)
            {
                e.GetComponent<NetworkObject>().Spawn(false);
            }
        }
        
    }

    public void SpawnEnemies(Vector3 Location)
    {
        if (!NetworkManager.Singleton.IsServer) return; //only host spawn enemies

        if(remainingEnemies.Count > 0)
        {
            var current = remainingEnemies.Dequeue();
            current.gameObject.SetActive(true);
            current.transform.position = Location;
            print("Enemy spawned from pool");
            current.NetworkObject.Spawn();

        }
    }

    public void AddToQueue(EnemyPathfinding enemy)
    {
        print("Enemy added to pool");
        remainingEnemies.Enqueue(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
