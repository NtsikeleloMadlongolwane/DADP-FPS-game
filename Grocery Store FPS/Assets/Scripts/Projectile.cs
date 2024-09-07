using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet has collided with a target
        if (collision.gameObject.CompareTag("Door"))
        {
            // Destroy the bullet GameObject
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Gun"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PickUp"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Podium"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("KillBox"))
        {
            Destroy(gameObject);
        }
    }
}
