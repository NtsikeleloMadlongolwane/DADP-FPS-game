using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{
    public List<GameObject> keys; // List of GameObjects to monitor

    public GameObject elevatorLock;
    public GameObject ememyRoomLock;
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
        else if (keys.Count == 1)
        {
            ememyRoomLock.SetActive(false);
        }
        else
        {

        }
        Debug.Log(keys.Count);
    }

}
