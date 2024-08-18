using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingScript : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attacckPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows; // how many times can the object be thrown
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0; //sets throw key to the right mouse button
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    public void Update()
    {
        if(Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        //Insatntiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attacckPoint.position, cam.rotation);

        // get rigidbody of the projectile
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate diraction
        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f)) // 500 is the range of the raycast.
        {
            forceDirection = (hit.point - attacckPoint.position).normalized;
        }
        //add force
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        //totalThrows--;

        // Implement ThrowCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
