using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public Transform bottomFloor;
    public Transform topFloor;
    public float speed = 2.0f;
    private Transform targetFloor;

    void Start()
    {
        targetFloor = transform; // Elevator starts at current position
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetFloor.position, speed * Time.deltaTime);
    }

    public void CallElevator()
    {
        targetFloor = bottomFloor;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetFloor = topFloor;
        }
    }
}


