using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePit : MonoBehaviour
{
    //public Transform player;
    //public Transform respawnPoint;

    public int damage = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy Made Contact");
            // Deal damage to the player
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Adjust the damage value as nee

            }
        }
    }

}
