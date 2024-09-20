using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumActivation : MonoBehaviour
{

    public GameObject boss;

    [Header("Podius Raise")]
    public GameObject fourPodiums;
    public List<GameObject> objectsToMonitor; // List of GameObjects to monitor
    public float timeForPodiumsToRise = 5f;
    public bool podiumsDestroyed = false;

    [Header("Camera Shake")]
    public Transform cameraTransform; // The camera's transform
    public float shakeDuration = 1f; // Duration of the shake
    public float shakeMagnitude = 0.1f; // Magnitude of the shake
    public float timeForShakeToStart = 10f;

    private Vector3 originalPosition;

    public void Start()
    {
        //camera shake for dramatic moments
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        originalPosition = cameraTransform.localPosition;

    }
    void Update()
    {
        // Iterate through the list in reverse to safely remove destroyed objects
        for (int i = objectsToMonitor.Count - 1; i >= 0; i--)
        {
            if (objectsToMonitor[i] == null)
            {
                // Remove the destroyed object from the list
                objectsToMonitor.RemoveAt(i);
            }
        }

        // Check if all objects are destroyed
        if (objectsToMonitor.Count == 0)
        {
            // Call the function
            Debug.Log("ALL KEY ARE USED");
            StartCoroutine(RaiseDoor(fourPodiums));
            StartShake();

            Destroy(fourPodiums, 20f); ///Destroyes the objects after the

        }
        if (podiumsDestroyed == true)
        {
            boss.SetActive(true);
        }
    }
    private IEnumerator RaiseDoor(GameObject door)
    {
        yield return new WaitForSeconds(timeForPodiumsToRise);  // pause before rest of the method runs

        float raiseAmount = 5f; // The total distance the door will be raised
        float raiseSpeed = 0.1f; // The speed at which the door will be raised
        Vector3 startPosition = door.transform.position; // Store the initial position of the door
        Vector3 endPosition = startPosition + Vector3.up * raiseAmount; // Calculate the final position of the door after raising

        // Continue raising the door until it reaches the target height
        while (door.transform.position.y < endPosition.y)
        {
            // Move the door towards the target position at the specified speed
            door.transform.position = Vector3.MoveTowards(door.transform.position, endPosition, raiseSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame before continuing the loop
        }
    }
    public void StartShake()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        yield return new WaitForSeconds(timeForShakeToStart); // pause before rest of the method runs
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            /// Shake varaibles
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.localPosition = new Vector3(0, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        podiumsDestroyed = true;

        cameraTransform.localPosition = originalPosition;
    }
}
