using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{

    public int damage = 10; // Amount of damage to deal

    // For 3D collisions, replace OnTriggerEnter2D with OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                Debug.Log("Player has been damaged");
            }
        }
    }
}