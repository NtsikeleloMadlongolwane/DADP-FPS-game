using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomManager : MonoBehaviour
{
    public GameObject walkingEnemy;
    public GameObject key;
    public GameObject roomDoor;
    public Transform keyPosition;
    public Transform closedDoorPosition;

    private Vector3 keySpot;
    private Vector3 closedDoorSpot;

    public Transform Spot1;
    public Transform Spot2;
    public Transform Spot3;
    public Transform Spot4;

    private Vector3 enemyPosition1;
    private Vector3 enemyPosition2;
    private Vector3 enemyPosition3;
    private Vector3 enemyPosition4;

    public TrapDoorTrigger trapDoorScript;
    

    public void Start()
    {
        // Set Key and clossed door spot
        keySpot = keyPosition.transform.position;
        closedDoorSpot = closedDoorPosition.transform.position;

        // sapwn new key
        Instantiate(key, keySpot, Quaternion.identity);

        // spawn enemies

        enemyPosition1 = Spot1.position;
        enemyPosition2 = Spot2.position;
        enemyPosition3 = Spot3.position;
        enemyPosition4 = Spot4.position;

        GameObject newObject1 = Instantiate(walkingEnemy, enemyPosition1 ,Quaternion.identity);
        GameObject newObject2 = Instantiate(walkingEnemy, enemyPosition2, Quaternion.identity);
        GameObject newObject3 = Instantiate(walkingEnemy, enemyPosition3, Quaternion.identity);
        GameObject newObject4 = Instantiate(walkingEnemy, enemyPosition4, Quaternion.identity);

        AddToTrapDoorSetUp(newObject1);
        AddToTrapDoorSetUp(newObject2);
        AddToTrapDoorSetUp(newObject3); 
        AddToTrapDoorSetUp(newObject4);
    }

    public void AddToTrapDoorSetUp(GameObject newObject)
    {

        if (trapDoorScript != null)
        {
            GameObject[] newArray = new GameObject[trapDoorScript.destructibleObjects.Length + 1];

            for (int i = 0; i < trapDoorScript.destructibleObjects.Length; i++) 
            {
                newArray[i] = trapDoorScript.destructibleObjects[i];
            }

            newArray[newArray.Length - 1] = newObject;
            trapDoorScript.destructibleObjects = newArray;
        }
        else
        {
            Debug.Log("TrapDoorTrigger is not assigned");
        }

    }
    public void ResetEnemyRoom()
    {
        // Spwan New Key at position
        if (key!=null)
        {
            Destroy(key);
        }

        Instantiate(key, keySpot, Quaternion.identity);
        // close door
        roomDoor.transform.position = closedDoorSpot;

        if(trapDoorScript.destructibleObjects.Length != 0)
        {
            Destroy(trapDoorScript.destructibleObjects[0]);
            Destroy(trapDoorScript.destructibleObjects[1]);
            Destroy(trapDoorScript.destructibleObjects[2]);
            Destroy(trapDoorScript.destructibleObjects[3]);
        }

       
        GameObject newObject1 = Instantiate(walkingEnemy, enemyPosition1, Quaternion.identity);
        GameObject newObject2 = Instantiate(walkingEnemy, enemyPosition2, Quaternion.identity);
        GameObject newObject3 = Instantiate(walkingEnemy, enemyPosition3, Quaternion.identity);
        GameObject newObject4 = Instantiate(walkingEnemy, enemyPosition4, Quaternion.identity);

        AddToTrapDoorSetUp(newObject1);
        AddToTrapDoorSetUp(newObject2);
        AddToTrapDoorSetUp(newObject3);
        AddToTrapDoorSetUp(newObject4);
    }
}
