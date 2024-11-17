using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EbenyHealth_Boss : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    public GameObject splash;
    public ParticleSystem splash2;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        splash2.Play();
        Debug.Log("Enemy health: " + currentHealth);
        if (currentHealth <= 0)
        {
            // Enemy dies
            Debug.Log("Enemy is dead!");
            Instantiate(splash, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
