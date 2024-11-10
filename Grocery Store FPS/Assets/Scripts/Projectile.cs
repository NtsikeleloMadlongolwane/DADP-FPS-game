using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public int damageAmount = 0;

   private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

            BossHealth bossHealth = other.gameObject.GetComponent<BossHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Player damaged by hurt box!");
            }
            else
            {
                bossHealth.TakeDamage(damageAmount);
            }
            //Destroy(this.gameObject);
        }

        else if(other.gameObject.CompareTag("Wall"))
        { 
         
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
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


        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = hitInfo.gameObject.GetComponent<EnemyHealth>();
            BossHealth bossHealth = hitInfo.gameObject.GetComponent<BossHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Player damaged by hurt box!");
                Destroy(gameObject);
            }
            else
            {
                bossHealth.TakeDamage(damageAmount);
            }
        }
            
        else if (hitInfo.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
  
    }
   
}
