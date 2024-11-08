using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth firstPersonControls = other.GetComponent<PlayerHealth>();
            firstPersonControls.SetRespawnPoint(transform.position);
            Debug.Log("New Resapwn Point has been Set");
        }
     
    }
}
