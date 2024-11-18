using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EbenyHealth_Boss : MonoBehaviour
{
    public BossMovements bossMovements;
    public int maxHealth = 500;
    private int currentHealth;
    public GameObject splash;
    void Start()
    {
        currentHealth = maxHealth;
        bossMovements = GetComponent<BossMovements>();
        Debug.Log("Enemy health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);
        if (currentHealth <= 0)
        {
            // Enemy dies
            Debug.Log("Enemy is dead!");
            GameObject EnemySplash = Instantiate(splash, transform.position, Quaternion.identity);
            bossMovements.BossDeath();
        }
    }
}
