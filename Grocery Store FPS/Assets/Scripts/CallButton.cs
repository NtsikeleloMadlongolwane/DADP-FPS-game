using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallButton : MonoBehaviour
{
  
    public Elevator elevator;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevator.CallElevator();
        }

    }
}
