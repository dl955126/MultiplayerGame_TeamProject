using UnityEngine;

namespace Anaiyah
{
    public class PlayerHealth : MonoBehaviour
    {
        public float maxHealth = 100f;

        public float currentHealth;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth <= 0)
            {
                Dead();
            }
        }

        private void Dead()
        {
           Destroy(gameObject);

        }
    }

}