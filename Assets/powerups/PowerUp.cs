using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject power;
    public GameObject player;
    public float PowerupTime;
    void Start()
    {

    }
    void Update()
    {
        PowerupTime -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision c1)
    {

        if (c1.transform == player.transform)
        {
            Destroy(power);
        }
    }


}
