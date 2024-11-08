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
    public ButtonHandler buttonHandler;
    public GameObject loseScreen;

    public int maxHealth = 50;
    private int currentHealth;

    public Vector3 respawnPoint;
    public Vector3 playerCheckPoint;
    private CharacterController characterController;

    private void Update()
    {
    }
    void Start()
    {
       characterController = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        TakeDamage(0);
        respawnPoint = transform.position;

    }

    public void TakeDamage(int damage)
    {

            currentHealth -= damage;
            Debug.Log("Player health: " + currentHealth);
            if (currentHealth <= 0)
            {
                // Player dies
                loseScreen.SetActive(true);
                Time.timeScale = 0;
            }

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

    public void Respawn()
    {
        characterController.enabled = false;
        this.transform.position = respawnPoint;
        characterController.enabled = true;
        Debug.Log("Player has been moverd");

    }
    public void SetRespawnPoint(Vector3 NewRespawnPoint)
    {
        respawnPoint = NewRespawnPoint;
        Debug.Log("Respawn Point is now " + (respawnPoint));
    }

    public void CheckPointSpawn()
    {
        loseScreen.SetActive(false);
        respawnPoint = playerCheckPoint;
        currentHealth = maxHealth;
        Respawn();
        Time.timeScale = 1;
    }
}
