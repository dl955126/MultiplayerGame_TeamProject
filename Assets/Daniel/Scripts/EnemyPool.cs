using System.Collections.Generic;
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
        }
        
    }

    public void SpawnEnemies(Vector3 Location)
    {
        print("remaining: " + remainingEnemies.Count);

        if(remainingEnemies.Count > 0)
        {
            var current = remainingEnemies.Dequeue();
            current.gameObject.SetActive(true);
            current.transform.position = Location;
        }
    }

    public void AddToQueue(EnemyPathfinding enemy)
    {
        print("adding " + enemy.gameObject.name);
        remainingEnemies.Enqueue(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
