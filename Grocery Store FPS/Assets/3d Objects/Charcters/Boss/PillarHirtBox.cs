using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarHirtBox : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            //FirstPersonControls firstPersonControls = other.GetComponent<FirstPersonControls>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
                Debug.Log("Player damaged by hurt box!");
            }

        }
    }
}
