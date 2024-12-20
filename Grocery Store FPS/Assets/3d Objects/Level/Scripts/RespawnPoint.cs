using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstPersonControls firstPersonControls = other.GetComponent<FirstPersonControls>();

            firstPersonControls.respawnPosition = gameObject.transform;
            Debug.Log("New Respawn Point Set to " + gameObject.transform.position);
        }
    }
}
