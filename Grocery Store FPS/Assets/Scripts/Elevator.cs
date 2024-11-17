using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public Transform bottomFloor;
    public Transform topFloor;
    public float speed = 5.0f;
    private Transform targetFloor;
    private int floor = 1;

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
            if (floor == 1)
            {
                targetFloor = topFloor;
                floor = 2;
            }
            else if (floor == 2)
            {
                targetFloor = bottomFloor;
                floor = 1;
            }
        }
    }
}


