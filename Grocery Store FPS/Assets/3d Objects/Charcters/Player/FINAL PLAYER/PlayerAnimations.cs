using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator playerAnim;
    public FirstPersonControls firstPersonControls;

    public bool isJumping;
    public bool isWalking;
    public bool isShooting;
    public bool isIdle;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        isJumping = firstPersonControls.isJumping;
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = firstPersonControls.isWalking;
        isShooting = firstPersonControls.isShooting;
        isIdle = !isWalking && !isJumping && !isShooting;

        if (isJumping)
        {
            playerAnim.Play("Jumping");
            isJumping=false;
        }
        else if (isWalking)
        {
            playerAnim.Play("Walking");
        }
        else if (isShooting)
        {
            playerAnim.Play("Shooting");
        }
        else if (isIdle)
        {
            playerAnim.Play("Idle");
        }
    }
}
