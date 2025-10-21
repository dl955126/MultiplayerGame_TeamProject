using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject player;

    private void OnCollisionEnter(Collision c1)
    {

        if (c1.transform == player.transform)
        {

            gameObject.SendMessage("PowerUp", "example");
        }
    }


}
