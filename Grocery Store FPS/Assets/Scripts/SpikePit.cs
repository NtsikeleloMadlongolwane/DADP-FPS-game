using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePit : MonoBehaviour
{
    public Transform player;
    public Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy Made Contact");
            player.position = respawnPoint.position;
            // Deal damage to the player
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                player.position = respawnPoint.position;
                playerHealth.TakeDamage(100); // Adjust the damage value as nee

            }
        }

    }
}
