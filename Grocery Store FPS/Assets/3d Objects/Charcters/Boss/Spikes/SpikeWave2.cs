using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWave2 : MonoBehaviour
{
    public Transform[] objects; // Array of objects to move
    public float height = 2f; // Maximum height to reach
    public float speed = 1f; // Movement speed
    public float delayBetweenObjects = 0.5f; // Delay between objects starting their motion

    public float timeBeforeWaveStarts = 0.5f;
    public float loopInterval = 0.5f;
    public GameObject beam;
    public ParticleSystem crystalBeam;

    private void Start()
    {
        crystalBeam.Stop();
    }
    public  IEnumerator LoopedWaveSequence()
    {
        crystalBeam.Play();
        beam.SetActive(true);

        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(2f);
            
            yield return StartCoroutine(RiseAndFallSequence(0.1f));          
        }

        beam.SetActive(false);
        crystalBeam.Stop();
    }

    public  IEnumerator RiseAndFallSequence(float delay)
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
   
    }


}
