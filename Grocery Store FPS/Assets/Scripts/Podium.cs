using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podium : MonoBehaviour
{
    public GameObject LockedKey;

    public bool UnlockesDoor = true;
    public string nameOfKey;

   // public GameObject doorBlockingCollider;

    public void Start()
    {
        LockedKey.SetActive(false);
        if (UnlockesDoor == true)
        {
           // doorBlockingCollider.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        { // Check if the colliding object has a specific tag
            if (collision.gameObject.name == nameOfKey)
            {
                // Perform an action if the tag matches
                Debug.Log("Collided with an object tagged as PickUp");
               // Destroy(collision.gameObject);

              // collision.gameObject.SetActive(false);
                LockedKey.SetActive(true);
               // doorBlockingCollider.SetActive(false);
               Destroy(collision.gameObject);
            }
            else
            {
               
            }
        }

    }
}
