using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{
    /*public List<GameObject> keys; // List of GameObjects to monitor

    public GameObject elevatorLock;
    private void Update()
    {
        for (int i = keys.Count - 1; i >= 0; i--)
        {
            if (keys[i] == null)
            {
                // Remove the destroyed object from the list
                keys.RemoveAt(i);
            }
        }
        if (keys.Count == 0)
        {
            elevatorLock.SetActive(false);
        }
        else
        {

        }
    }*/

    public List<GameObject> keys; // List of GameObjects to monitor
    public GameObject elevatorLock;

    private void Update()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i] == null)
            {
                // Remove the destroyed object from the list
                keys.RemoveAt(i);
                i--; // Adjust the index after removal
            }
        }
        if (keys.Count == 0)
        {
            elevatorLock.SetActive(false);
        }
    }

}
