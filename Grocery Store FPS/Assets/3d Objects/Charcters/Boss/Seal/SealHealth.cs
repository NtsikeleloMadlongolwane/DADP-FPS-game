using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealHealth : MonoBehaviour
{

    public int maxHealth = 50;
    private int currentHealth;
    public ParticleSystem splash;
    public ParticleSystem crystalExplotion;

    private GameObject topFloor;
    private GameObject topFloor2;
    public bool sealisbroken;

    public ScreenShake screenShake;

    void Start()
    {
        currentHealth = maxHealth;
        crystalExplotion.Stop();
        sealisbroken = false;
        topFloor = GameObject.Find("TopFloorCage");
        topFloor2 = GameObject.Find("Floor.007");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Deal damage to the enemy

            Instantiate(splash, transform.position, Quaternion.identity);

            TakeDamage(10); // Adjust the damage value as needed


            // Destroy the bullet
            Destroy(collision.gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy health: " + currentHealth);
        if (currentHealth <= 0)
        {
            // Enemy dies

            //Destroy(gameObject);
            StartCoroutine(screenShake.Shake(15f, 0.8f));
            StartCoroutine(DisableThings());

        }
    }
    public IEnumerator DisableThings()
    {
        yield return new WaitForSeconds(5);
        topFloor.SetActive(false);
        topFloor2.SetActive(false);
        yield return new WaitForSeconds(8);
        sealisbroken = true;
        //gameObject.SetActive(false);    
        Debug.Log("Seal is Broken");
    }
}
