using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public GameObject enemies;
    public GameObject trapDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trapDoor.SetActive(true);
            Debug.Log("Trap door activated");
            enemies.SetActive(true);
        }
   
    }
}
    

