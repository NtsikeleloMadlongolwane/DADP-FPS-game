using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 50;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);
        if (currentHealth <= 0)
        {
            // Enemy dies
            Debug.Log("Enemy is dead!");
            Destroy(gameObject);
        }
    }
}
