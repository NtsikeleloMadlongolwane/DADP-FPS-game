using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public int maxHealth = 500;
    private int currentHealth;
    public GameObject splash;
    public bool isBossALive = true;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);
        Instantiate(splash,gameObject.transform.position, Quaternion.identity);
        if (currentHealth <= 0)
        {
            isBossALive = false;
            Debug.Log("Boss is dead!");
        }
    }
}
