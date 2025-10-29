using UnityEngine;

public class PowerUpA : MonoBehaviour
{
    float currentHealth;
    float healAmount = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void GetHeal()
    {
        currentHealth+= healAmount;
    }
}
