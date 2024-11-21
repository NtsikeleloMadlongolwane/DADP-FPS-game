using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Animator anim;

    public bool isIdle = false;
    public bool isSpawning = false;
    public bool isTargeting = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargeting)
        {
            anim.Play("TargetShake");
        }
        else if (isIdle)
        {
            anim.Play("Boss Idle");
        }
        else if (isSpawning)
        {
            anim.Play("SpawnShake");
        }
    }
}
