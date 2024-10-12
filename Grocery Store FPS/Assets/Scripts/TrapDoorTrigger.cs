using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorTrigger : MonoBehaviour
{
    public TrapDoor trapdoor;

    public GameObject[] EnemiesInRoom;
    private int counter = 0;

    void OnTriggerEnter(Collider other)
    {
        counter++;
        if (other.CompareTag("Player"))
        {
            if (counter == 1)
            {
                trapdoor.LowerTrapdoor();
            }
        }
    }

    void Update()
    {
        bool allDestroyed = true;
        foreach (GameObject obj in EnemiesInRoom)
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
