using UnityEngine;
using UnityEngine.Events;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifeTime;
    [SerializeField] int bulletDamage;
    float bulletElapsed;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletElapsed = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        bulletElapsed += Time.deltaTime;

        if(bulletElapsed > bulletLifeTime)
        {
            Destroy(gameObject);

        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyPathfinding>())
        {
            collision.gameObject.GetComponent<EnemyPathfinding>().OnShot(bulletDamage);
            Destroy(gameObject);
        }
    }


}
