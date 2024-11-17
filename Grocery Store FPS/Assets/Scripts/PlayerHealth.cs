using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100; // The player's maximum health
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Initialize the player's health to the maximum
        Debug.Log("Player Health is Now" + currentHealth);
    }

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Subtract the damage from the current health

        // Check if the player's health has reached zero or below
        if (currentHealth <= 0)
        {
            Die(); // Call the Die method if the player's health is zero or below
        }

        Debug.Log("Player Health is Now" +  currentHealth);
    }

    private void Die()
    {
        // Handle player death (e.g., play death animation, respawn, end game)
        Debug.Log("Player has died"); // For now, we'll just log a message
        // Implement additional logic here for what happens when the player dies
    }
}

