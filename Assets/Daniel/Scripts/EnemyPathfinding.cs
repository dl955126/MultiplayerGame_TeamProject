using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    [Header("Enemy Variables")]
    [SerializeField] float speed;
    Rigidbody rb;

    [Header("Navmesh")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] NavMeshPath navPath;
    [SerializeField] Transform playerTarget;
    Queue<Vector3> remainingCorners;
    Vector3 currentCorner;
    [SerializeField] float timeToRecalcPath;
    float timeElapsedPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        navPath = new NavMeshPath();
        remainingCorners = new Queue<Vector3>();

        CreatePath();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsedPath += Time.deltaTime;

        if (timeElapsedPath > timeToRecalcPath)
        {
            remainingCorners.Clear();
            CreatePath();
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

    public void CreatePath()
    {
        if (agent.CalculatePath(playerTarget.position, navPath))
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
