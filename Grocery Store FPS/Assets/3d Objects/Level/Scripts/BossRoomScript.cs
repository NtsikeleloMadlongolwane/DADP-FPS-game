using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomScript : MonoBehaviour
{
    public GameObject Boss;
    public GameObject BossSeal;
    public Transform BossPosition;

    public GameObject TopFloorCage;
    public GameObject ElevatorDoor;
    public Transform DoorPostion;


    public void Start()
    {
        Instantiate(Boss, BossPosition.position, Quaternion.identity);
        Instantiate(BossSeal, BossPosition.position, Quaternion.identity);
    }

    public void ResetBossRoom()
    {
        TopFloorCage.SetActive(true);
        ElevatorDoor.transform.position = DoorPostion.transform.position;

        if(Boss != null)
        {
            Destroy(Boss);
            Destroy(BossSeal);
        }

        Instantiate(Boss, BossPosition.position, Quaternion.identity);
        Instantiate(BossSeal, BossPosition.position, Quaternion.identity);
    }

}
