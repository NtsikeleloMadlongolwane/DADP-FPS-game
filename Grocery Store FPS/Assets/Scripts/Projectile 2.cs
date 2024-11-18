using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    public int damage = 10;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        { // Deal damage to the enemy
           EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
           if (enemyHealth != null) 
            { 
                enemyHealth.TakeDamage(damage); 
            }

            // Destroy the projectile upon collision
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Boss"))
        {
            // Deal damage to the enemy
           BossMovements enemyHealth = other.GetComponent<BossMovements>();
           if (enemyHealth != null) 
            { 
                enemyHealth.TakeDamage(damage); 
            }

            // Destroy the projectile upon collision
            Destroy(gameObject);
        }

    }
}