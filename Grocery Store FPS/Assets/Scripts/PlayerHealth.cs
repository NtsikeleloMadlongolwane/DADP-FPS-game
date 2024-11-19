using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Transform teleporterPosition;
    public int maxHealth = 50; // The player's maximum health
    public int currentHealth;


    [Header("UI")]
    public GameObject boss;
    public bool isBossDead;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    [Header("HUD")]

    public GameObject[] HeathHUD;
    public GameObject[] HealsHuD;
    public GameObject[] PlayerCondtion;
    public FirstPersonControls firstPersonControls;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Initialize the player's health to the maximum
        Debug.Log("Player Health is Now" + currentHealth);
        firstPersonControls = GetComponent<FirstPersonControls>();
    }

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StartCoroutine(DeathSequence()); // Call the Die method if the player's health is zero or below
            
        }

        Debug.Log("Player Health is Now" +  currentHealth);
    }

    public void Update()
    {
        // HUD

        // UI
        int maskCounter = currentHealth / 10;

        // Masks

        if (maskCounter == 5)
        {
            for (int i = 0; i < maskCounter; i++)
            {
                HeathHUD[i].SetActive(true);
            }
        }
        else if (maskCounter == 4)
        {
            HeathHUD[maskCounter].SetActive(false);
            for (int i = 0; i < maskCounter; i++)
            {
                HeathHUD[i].SetActive(true);
            }
        }
        else if (maskCounter == 3)
        {
            HeathHUD[maskCounter].SetActive(false);
            for (int i = 0; i < maskCounter; i++)
            {
                HeathHUD[i].SetActive(true);
            }
        }
        else if (maskCounter == 2)
        {
            HeathHUD[maskCounter].SetActive(false);
            for (int i = 0; i < maskCounter; i++)
            {
                HeathHUD[i].SetActive(true);
            }
        }
        else if (maskCounter == 1)
        {
            HeathHUD[maskCounter].SetActive(false);
            for (int i = 0; i < maskCounter; i++)
            {
                HeathHUD[i].SetActive(true);
            }
        }
        else if (maskCounter == 0)
        {
            HeathHUD[maskCounter].SetActive(false);
        }

        int firstAid = firstPersonControls.firstAid;
        // heals 
        if (firstAid == 2)
        {
            for (int x = 0; x < firstAid; x++)
            {
                HealsHuD[x].SetActive(true);
            }
        }
        else if (firstAid == 1)
        {
            HealsHuD[firstAid].SetActive(false);
            for (int x = 0; x < firstAid; x++)
            {
                HealsHuD[x].SetActive(true);
            }
        }
        else if (firstAid == 0)
        {
            HealsHuD[firstAid].SetActive(false);
        }


        // Check boss

        if(boss == null && !isBossDead)
        {
            Time.timeScale = 0f;
            firstPersonControls.canMove = false;
            firstPersonControls.gamePaused = true;
            WinScreen.SetActive(true);
            isBossDead = true;
        }
    }
    public IEnumerator DeathSequence()
    {
        //stop movement
        FirstPersonControls firstPersonControls = GetComponent<FirstPersonControls>();
        firstPersonControls.canMove = false;

        // Reset Respawn
        firstPersonControls.respawnPosition = teleporterPosition.transform;

        // death animation
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        LoseScreen.SetActive(true);
        yield return null;
    }
}

