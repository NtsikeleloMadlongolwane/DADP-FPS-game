using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePrefab : MonoBehaviour
{
    public Transform StartSpot;
    public Transform EndSpot;
    public float speed = 10;
    public GameObject Spike;
    private void Start()
    {   
        Spike.transform.position = StartSpot.transform.position;
        StartCoroutine(MoveSpike());
    }

   // The IEnumerator method that moves the object
    private IEnumerator MoveSpike()
    {
        yield return new WaitForSeconds(2f);

        Vector3 originalPosition = StartSpot.transform.position;

        // Move to the target position
        while (Vector3.Distance(Spike.transform.position, EndSpot.position) > 0.01f)
        {
            Spike.transform.position = Vector3.MoveTowards(Spike.transform.position, EndSpot.position, speed * Time.deltaTime);
            yield return null;
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(3);

        // Move back to the original position
        while (Vector3.Distance(Spike.transform.position, originalPosition) > 0.01f)
        {
            Spike.transform.position = Vector3.MoveTowards(Spike.transform.position, originalPosition, speed * Time.deltaTime);
            yield return null;
        }

        // Wait for another 2 seconds
        yield return new WaitForSeconds(2);; 
    }
}

