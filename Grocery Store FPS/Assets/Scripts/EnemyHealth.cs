using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("It doest work yet");
        }
    }


    private void Die()
    {
        Debug.Log("Player died!");
        Destroy(gameObject);
        // Add your death logic here
    }
}
