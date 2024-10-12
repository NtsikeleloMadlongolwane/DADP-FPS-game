using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWave2 : MonoBehaviour
{
    public Transform[] objects; // Array of objects to move
    public float height = 2f; // Maximum height to reach
    public float speed = 1f; // Movement speed
    public float delayBetweenObjects = 0.5f; // Delay between objects starting their motion
    public GameObject seal;

    public float timeBeforeWaveStarts = 3f;
    public float loopInterval = 5f;
    public GameObject beam;


    void Start()
    {
        StartCoroutine(LoopedWaveSequence());
    }


        /*  void OnCollisionEnter(Collision collision)
          {
              if (collision.gameObject.CompareTag("Player")) // Adjust the tag as needed
              {
                  StartCoroutine(RiseAndFallSequence(timeBeforeWaveStarts));
              }
          }*/

        IEnumerator LoopedWaveSequence()
    {
        
        yield return new WaitForSeconds(timeBeforeWaveStarts);

        while (true)
        {
            beam.SetActive(true);
            yield return StartCoroutine(RiseAndFallSequence(3));
            yield return new WaitForSeconds(loopInterval);
        }
    }

        IEnumerator RiseAndFallSequence(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (Transform obj in objects)
        {
            Vector3 startPos = obj.position;
            Vector3 endPos = new Vector3(startPos.x, startPos.y + height, startPos.z);

            // Move up
            while (obj.position.y < endPos.y)
            {
                obj.position = Vector3.MoveTowards(obj.position, endPos, speed * Time.deltaTime);
                yield return null;
            }

            // Move down
            while (obj.position.y > startPos.y)
            {
                obj.position = Vector3.MoveTowards(obj.position, startPos, speed * Time.deltaTime);
                yield return null;
            }

            // Delay before moving the next object
            yield return new WaitForSeconds(delayBetweenObjects);
        }
        beam.SetActive(false);
    }
}
