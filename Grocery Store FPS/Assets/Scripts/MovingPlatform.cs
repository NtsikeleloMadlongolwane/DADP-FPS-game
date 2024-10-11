using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // The starting point
    public Transform pointB; // The ending point
    public float speed = 2f; // Speed of the platform

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = pointB.position;
    }

    void Update()
    {
        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the platform has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Switch the target position
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }
}
