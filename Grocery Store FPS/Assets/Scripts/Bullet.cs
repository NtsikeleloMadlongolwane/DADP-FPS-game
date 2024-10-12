using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Destroy the bullet after it hit
    }
}