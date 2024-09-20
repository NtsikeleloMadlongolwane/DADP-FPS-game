using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    // public Transform targetPoint;

    public GameObject PauseScreen;

    public int maxHealth = 100;
    private int currentHealth;
    
    
    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("It doest work yet");
        }
    }

   
    private void Die()
    {
        Debug.Log("Player died!");
        // Add your death logic here
        Time.timeScale = 0f;
        PauseScreen.SetActive(true);
    }

 
}
