using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public int maxHealth = 500;
    private int currentHealth;
    public GameObject splash;

    public bool isBossAlive = true;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Boss Health: "+ currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);
        Instantiate(splash, gameObject.transform.position, Quaternion.identity);
        if (currentHealth <= 0)
        { 
            isBossAlive = false;
            Debug.Log("Enemy is dead!");
        }
    }
}
