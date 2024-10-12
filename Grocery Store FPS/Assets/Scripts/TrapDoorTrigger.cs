using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorTrigger : MonoBehaviour
{
    public TrapDoor trapdoor;
    public GameObject[] destructibleObjects;

    private int counter = 0;

    void OnTriggerEnter(Collider other)
    {
        counter++;
        if (other.CompareTag("Player"))
        {
            if(counter == 1)
            {
                trapdoor.LowerTrapdoor();
            }
           
        }

        Debug.Log("Player Tiggered Trap door!");
    }

    void Update()
    {
        bool allDestroyed = true;
        foreach (GameObject obj in destructibleObjects)
        {
            if (obj != null)
            {
                allDestroyed = false;
                break;
            }
        }

        if (allDestroyed)
        {
            trapdoor.RaiseTrapdoor();
        }
    }
}
