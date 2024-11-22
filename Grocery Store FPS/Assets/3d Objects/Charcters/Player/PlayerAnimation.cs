using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimations;
    public FirstPersonControls FirstPersonControls;

    public bool isWalking;
    public bool isJumping;
    void Start()
    {
        playerAnimations = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
       // isJumping = FirstPersonControls.isJumping;
        //playerAnimations.SetBool("isWalking", FirstPersonControls.isWalking);
        //playerAnimations.SetBool("isJumping", FirstPersonControls.isJumping);
    }
}
