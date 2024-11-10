using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    public int maxHealth = 500;
    private int currentHealth;
    public GameObject splash;
<<<<<<< HEAD
    public bool isBossALive = true;
=======
>>>>>>> parent of 43aa5cf (push)

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);
<<<<<<< HEAD
        Instantiate(splash,gameObject.transform.position, Quaternion.identity);
        if (currentHealth <= 0)
        {
            isBossALive = false;
            Debug.Log("Boss is dead!");
=======
        if (currentHealth <= 0)
        { 
            Debug.Log("Enemy is dead!");
>>>>>>> parent of 43aa5cf (push)
        }
    }
}
