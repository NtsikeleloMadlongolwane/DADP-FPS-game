using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageAmount = 0; 
        void OnTriggerEnter(Collider hitInfo)
        {
            // Add logic for what happens when the bullet hits something
            if (hitInfo.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hitInfo.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount);
                    Debug.Log("Player damaged by hurt box!");
                }
            }
            Destroy(gameObject);
        }
   
}
