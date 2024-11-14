using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameObject player;
    public Transform targetPoint;
    public int damageAmount = 10; // Amount of damage to deal to the player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Player damaged by hurt box!");
            }


        }
    }

    public void PlayerRespawn()
    {
        if ( player!= null && targetPoint != null)
        {
            player.transform.position = targetPoint.position;
            Debug.Log("Player moved to target point!");
        }
        else
        {
            Debug.LogWarning("Player or target point not assigned.");
        }
    }
}
