using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePit : MonoBehaviour
{
    public int damage = 1;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided wiyth spike pit");
        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("Enemy Made Contact");

            // Deal damage to the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            playerHealth.TakeDamage(1); // Adjust the damage value as needed

           

        }
    }

}
