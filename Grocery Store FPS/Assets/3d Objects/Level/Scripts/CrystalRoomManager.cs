using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CrystalRoomManager : MonoBehaviour
{
    public GameObject key;
    public GameObject roomDoor;
    public Transform keyPosition;
    public Transform closedDoorPosition;

    private Vector3 keySpot;
    private Vector3 closedDoorSpot;

    public void Start()
    {
        keySpot = keyPosition.transform.position;
        closedDoorSpot = closedDoorPosition.transform.position;
        // Spwan New Key at position
        Instantiate(key, keySpot, Quaternion.identity);

    }
    public void ResetCrystalRoom()
    {
        // Spwan New Key at position
        if(key != null)
        {
            Destroy(key);
        }
        Instantiate(key, keySpot, Quaternion.identity);
        // close door
        roomDoor.transform.position = closedDoorSpot;
    } 
}
