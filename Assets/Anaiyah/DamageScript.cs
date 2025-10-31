using UnityEngine;
using Anaiyah;


    public class DamageScript : MonoBehaviour
    {
        public float damageAmount = 10f;
        
        private void OnCollisionEnter(Collision collision)
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }
    }

