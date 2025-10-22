using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject player;
    public float PowerupTime;
    void Start()
    {
        PowerupTime = 0;
    }
    void Update()
    {
        PowerupTime -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision c1)
    {

        if (c1.transform == player.transform)
        {

            gameObject.SendMessage("PowerUp", "example");
            Destroy(gameObject);
        }
    }


}
