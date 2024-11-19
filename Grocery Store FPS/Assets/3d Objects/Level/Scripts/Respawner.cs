using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject crystalKeySpot;
    public GameObject voidKeySpot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstPersonControls firstPersonControls = other.GetComponent<FirstPersonControls>();

            firstPersonControls.Respawn();
        }
        else if (other.CompareTag("PickUp"))
        {
            if(other.gameObject.name == "StarkKey Final")
            {
                // check if spinning thing is ennabled
                RotateItems rotateItems = other.GetComponent<RotateItems>();

                if (rotateItems.enabled == true)
                {
                   Rigidbody rb = other.GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                    other.gameObject.transform.position = crystalKeySpot.transform.position;
                }
            }
            else
            {
                other.gameObject.transform.position = voidKeySpot.transform.position;
            }
            
        }
    }
}
