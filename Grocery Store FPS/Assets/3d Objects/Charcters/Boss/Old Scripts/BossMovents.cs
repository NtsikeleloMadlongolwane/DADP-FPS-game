using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovents : MonoBehaviour
{
    public Transform battleArena;
    public GameObject seal;
    public float moveSpeed = 5f; // Adjustable speed

    private bool isMoving = false;

    void Update()
    {
        if (seal == null && !isMoving)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            MoveToBattleArena();
        }
    }

    void MoveToBattleArena()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, battleArena.position, step);
        if (transform.position == battleArena.position)
        {
            isMoving = false;
            Debug.Log("Boss reached the battle arena");
        }

    } 
}
