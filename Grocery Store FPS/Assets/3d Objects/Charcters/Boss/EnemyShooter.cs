using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject objectPrefab; // The prefab to instantiate at the player's position
    public Transform firePoint;
    public Transform player;
    public float bulletSpeed = 10f;
    public float timeBetweenShots = 2f;

    private float nextFireTime;
    private GameObject currentTarget;

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            StartCoroutine(InstantiateAndShoot());
            nextFireTime = Time.time + timeBetweenShots;
        }
    }

    private IEnumerator InstantiateAndShoot()
    {
        // Instantiate the object at the player's position
        currentTarget = Instantiate(objectPrefab, player.position, Quaternion.identity);

        // Wait for one second
        yield return new WaitForSeconds(1f);

        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (currentTarget.transform.position - firePoint.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }
}
        