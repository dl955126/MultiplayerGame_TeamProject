using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBullet : NetworkBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifeTime;
    [SerializeField] int bulletDamage;
    float bulletElapsed;
    bool isDespawned = false;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnNetworkSpawn()
    {
        
        rb = GetComponent<Rigidbody>();

        if (IsServer)
        {
            bulletElapsed = 0;
        }
    }

    private void Update()
    {
        if (!IsServer || isDespawned) return;

        bulletElapsed += Time.deltaTime;

        if(bulletElapsed > bulletLifeTime)
        {
            DeSpawnBullet();

        }
    }

    private void FixedUpdate()
    {
        if (!IsServer) return;
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    /*
    [Rpc(SendTo.Server)]
    public void ServerDestroyBulletRpc()
    {
        gameObject.GetComponent<NetworkObject>().Despawn();
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsServer || isDespawned) return;
        if (collision.gameObject.GetComponent<EnemyPathfinding>())
        {
            collision.gameObject.GetComponent<EnemyPathfinding>().OnShot(bulletDamage);
            
        }
    }

    void DeSpawnBullet()
    {
        if(isDespawned) return;
        isDespawned = true;

        GetComponent<NetworkObject>().Despawn();
    }


}
