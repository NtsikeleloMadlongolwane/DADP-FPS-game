using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorTrigger : MonoBehaviour
{
    public TrapDoor trapdoor;
    public GameObject[] destructibleObjects;

    private int counter = 1;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(counter == 1)
            {
                trapdoor.LowerTrapdoor();

                destructibleObjects[0].SetActive(true);
                destructibleObjects[1].SetActive(true);
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
            counter++;
        }
    }
}
