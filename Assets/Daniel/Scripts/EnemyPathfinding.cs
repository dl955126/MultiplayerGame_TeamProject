using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : NetworkBehaviour
{
    [Header("Enemy Variables")]
    [SerializeField] float speed;
    [SerializeField] int maxHealth = 10;
    int currentEnemyHealth;
    
    Rigidbody rb;

    [Header("Navmesh")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] NavMeshPath navPath;
    [SerializeField] NewPlayerInputs playerTarget;
    Queue<Vector3> remainingCorners;
    Vector3 currentCorner;
    [SerializeField] float timeToRecalcPath;
    float timeElapsedPath;

    EnemyPool pool;

    bool hasFoundPlayer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentEnemyHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
        navPath = new NavMeshPath();
        remainingCorners = new Queue<Vector3>();

        if (hasFoundPlayer)
        {
            CreatePath();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //find player at runtime
        if (!hasFoundPlayer)
        {
            FindPlayer();
        }

        timeElapsedPath += Time.deltaTime;

        if (timeElapsedPath > timeToRecalcPath)
        {
            remainingCorners.Clear();
            if (hasFoundPlayer)
            {
                CreatePath();
            }
            timeElapsedPath = 0;

        }

        Vector3 newFoward = (currentCorner - transform.position).normalized;
        newFoward.y = 0;
        transform.forward = newFoward;

        float distanceToCorner = Vector3.Distance(transform.position, currentCorner);
        if (distanceToCorner < 2)
        {
            if(remainingCorners.Count > 0)
            {
                currentCorner = remainingCorners.Dequeue();
            }
        }


    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * speed;
    }
    public void FindPlayer()
    {
        playerTarget = FindAnyObjectByType<NewPlayerInputs>();
        hasFoundPlayer = true;
    }


    public void CreatePath()
    {
        if (agent.CalculatePath(playerTarget.transform.position, navPath))
        {
            Debug.Log("Path found");
            foreach (Vector3 corner in navPath.corners)
            {
                remainingCorners.Enqueue(corner);

            }

            if (remainingCorners.Count > 0)
            {
                currentCorner = remainingCorners.Dequeue();
            }
        }
    }

    public void OnShot(int bulletDamage)
    {
        currentEnemyHealth -= bulletDamage;

        if (currentEnemyHealth <= 0) 
        {
            DespawnRpc();
        }
    }

    [Rpc(SendTo.Server)]
    public void DespawnRpc()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<NetworkObject>().Despawn(false);
    }

    private void OnDisable()
    {
        if (pool != null)
        {
            pool.AddToQueue(this);
        }
    }


    public void SetPool(EnemyPool enemyPool)
    {
        pool = enemyPool;
    }


    //draw out the path for the enemy
    private void OnDrawGizmos()
    {
        
        if(navPath == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        foreach(Vector3 node in navPath.corners)
        {
            Gizmos.DrawWireSphere(node, 0.5f);
        }

    }
}
