using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    private Rigidbody rb;

    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // this makes sure the projectile only sticks to the first target it hits

        if (targetHit)
            return;
        
        else
        
            targetHit = true;

            rb.isKinematic = true;

            transform.SetParent(collision.transform);
        
    }
}
