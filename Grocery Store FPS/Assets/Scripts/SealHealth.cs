using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealHealth : MonoBehaviour
{

    public int maxHealth = 50;
    private int currentHealth;
    public ParticleSystem splash;

    void Start()
    {
        currentHealth = maxHealth;
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Deal damage to the enemy

            Instantiate(splash, transform.position, Quaternion.identity);

            TakeDamage(10); // Adjust the damage value as needed


            // Destroy the bullet
            Destroy(collision.gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);
        if (currentHealth <= 0)
        {
            // Enemy dies
            Debug.Log("Enemy is dead!");
            Instantiate(splash, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
