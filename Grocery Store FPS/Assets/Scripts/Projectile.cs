using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public int damageAmount = 0;


    public float knockbackForce = 10f;
    public float upwardForce = 5f;

   private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                Vector3 knockbackDirection = (other.transform.position - transform.position);//.normalized;
                knockbackDirection.y = 1; // Add an upward component to the knockback direction
                enemyRigidbody.AddForce(knockbackDirection * knockbackForce + Vector3.up * upwardForce, ForceMode.Impulse);
                Debug.Log("Enemy knocked back and upwards!");
            }
            else
            {
                Debug.Log("No Rigidbody found on the enemy.");
            }

            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Player damaged by hurt box!");
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
            Rigidbody enemyRigidbody = hitInfo.gameObject.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                Vector3 knockbackDirection = (hitInfo.transform.position - transform.position);//.normalized;
                knockbackDirection.y = 1; // Add an upward component to the knockback direction
                enemyRigidbody.AddForce(knockbackDirection * knockbackForce + Vector3.up * upwardForce, ForceMode.Impulse);
                Debug.Log("Enemy knocked back and upwards!");
            }
            else
            {
                Debug.Log("No Rigidbody found on the enemy.");
            }

            EnemyHealth enemyHealth = hitInfo.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Player damaged by hurt box!");
                Destroy(gameObject);
            }
        }
            
        else if (hitInfo.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
  
    }
   
}
