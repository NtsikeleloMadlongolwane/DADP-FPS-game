using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    private int count = 1;
    public GameObject enemies;
    public GameObject trapDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (count == 1)
            {
                trapDoor.SetActive(true);
                Debug.Log("Trap door activated");
                enemies.SetActive(true);
                count = 0;
            }
            else
            {

            }

        }
   
    }
}
    

