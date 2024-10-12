using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public Transform targetPosition;
    public Transform initialPosition;
    public float loweringSpeed = 2f;
    public float raisingSpeed = 2f;

    private bool lowering = false;
    private bool raising = false;

    void Update()
    {
        if (lowering)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, loweringSpeed * Time.deltaTime);
        }
        else if (raising)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition.position, raisingSpeed * Time.deltaTime);
        }
    }

    public void LowerTrapdoor()
    {
        lowering = true;
        raising = false;
    }

    public void RaiseTrapdoor()
    {
        raising = true;
        lowering = false;
    }
}
    

