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

    public ScreenShake screenShake;
    private bool isTakingDamage = false;

    public int maxHealth = 50;
    private int currentHealth;

    private void Update()
    {
    }
    void Start()
    {
        currentHealth = maxHealth;
        TakeDamage(0);
        screenShake = Camera.main.GetComponent<ScreenShake>();
    }

    public void TakeDamage(int damage)
    {
        if (!isTakingDamage)
        {
            currentHealth -= damage;
            Debug.Log("Player health: " + currentHealth);
            if (currentHealth <= 0)
            {
                buttonHandler.Die();
                // Player dies
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
            StartCoroutine(HandleDamage(0,1, 0.1f));
        }
    }

    public void Healing()
    { 
        currentHealth = currentHealth + 10;
        Debug.Log("Player Healed!");
          
    }
    private IEnumerator HandleDamage(float delay, float shakeDuration, float shakeMagnitude)
    {
        isTakingDamage = true;
        Time.timeScale = 0;
        yield return new WaitForSeconds(delay);
   
        StartCoroutine(screenShake.Shake(shakeDuration, shakeMagnitude));
        isTakingDamage = false;
        Time.timeScale = 1;
    }
}
