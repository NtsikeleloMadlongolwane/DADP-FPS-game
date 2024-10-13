using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    // public Transform targetPoint;
    [Header("UI")]
    public GameObject[] health;


    public int maxHealth = 50;
    private int currentHealth;

    private void Update()
    {
        Debug.Log(currentHealth);
    }
    void Start()
    {
        currentHealth = maxHealth;
        TakeDamage(0);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player health: " + currentHealth);
        if (currentHealth <= 0)
        {
            // Player dies
            Debug.Log("Player is dead!");
            Time.timeScale = 0;

        }
        Debug.Log("Player got hit!");


        if (currentHealth == 50)
        {
            health[0].SetActive(true);
            health[1].SetActive(true);
            health[2].SetActive(true);
            health[3].SetActive(true);
            health[4].SetActive(true);
        }
        else if (currentHealth == 40)
        {
            health[0].SetActive(true);
            health[1].SetActive(true);
            health[2].SetActive(true);
            health[3].SetActive(true);
            health[4].SetActive(false);
        }
        else if (currentHealth == 30)
        {
            health[0].SetActive(true);
            health[1].SetActive(true);
            health[2].SetActive(true);
            health[3].SetActive(false);
            health[4].SetActive(false);
        }
        else if (currentHealth == 20)
        {
            health[0].SetActive(true);
            health[1].SetActive(true);
            health[2].SetActive(false);
            health[3].SetActive(false);
            health[4].SetActive(false);
        }
        else if (currentHealth == 10)
        {
            health[0].SetActive(true);
            health[1].SetActive(false);
            health[2].SetActive(false);
            health[3].SetActive(false);
            health[4].SetActive(false);
        }
        else if (currentHealth == 0)
        {
            health[0].SetActive(false);
            health[1].SetActive(false);
            health[2].SetActive(false);
            health[3].SetActive(false);
            health[4].SetActive(false);
        }
    }

    public void Healing()
    { 
        currentHealth = currentHealth + 10;
        Debug.Log("Player Healed!");
          
    }


}
