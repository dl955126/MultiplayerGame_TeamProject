using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Anaiyah
{
    public class PlayerHealth : NetworkBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth;
        public float Iframe = 1.5f;
        public float elapsedDamage;
        bool canBeDamaged;

        [SerializeField] Image healthImage;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public override void OnNetworkSpawn()
        {
            currentHealth = maxHealth;

            if (IsOwner)
            {
                healthImage = GameObject.FindWithTag("Health").GetComponent<Image>();
            }
            canBeDamaged = true;
        }

        private void Update()
        {
            if(!IsOwner) return;

            healthImage.fillAmount = currentHealth / maxHealth;

            if(!canBeDamaged)
            {
                elapsedDamage += Time.deltaTime;

                if(elapsedDamage > Iframe)
                {
                    canBeDamaged = true;
                    elapsedDamage = 0;
                }

            }
        }

        public void TakeDamage(float damage)
        {
            canBeDamaged = false;
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth <= 0)
            {
                Dead();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<EnemyPathfinding>() && canBeDamaged)
            {
                TakeDamage(collision.gameObject.GetComponent<EnemyPathfinding>().enemyDamage);
            }
        }

        //need to fix
        private void Dead()
        {
            

        }
    }

}