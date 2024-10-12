using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    public Transform groundCheck;  // Empty GameObject placed at the character's feet
    public float groundDistance = 0.1f;  // Distance to check for ground
    public LayerMask groundMask;   // Layer that represents the ground

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator not found on the object.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check: using a small sphere at the character's feet to detect ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Detecting if the player is pressing the up arrow key or moving forward using the vertical axis
        bool forwardPress = Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0;
        bool isWalking = animator.GetBool("isWalking");

        // Detecting space bar press for jumping
        bool jumpPress = Input.GetKeyDown(KeyCode.Space); // Detects when space bar is initially pressed
        bool isJumping = animator.GetBool("isJumping");

        // Walking logic
        if (!isWalking && forwardPress)
        {
            animator.SetBool("isWalking", true);
        }
        else if (isWalking && !forwardPress)
        {
            animator.SetBool("isWalking", false);
        }

        // Jumping logic: set jump to true when space is pressed if the player is grounded
        if (jumpPress && isGrounded)
        {
            animator.SetBool("isJumping", true);
            Debug.Log("Jump key pressed.");
        }

        // Reset the jump state when the character lands
        if (isJumping && isGrounded)
        {
            animator.SetBool("isJumping", false);
            Debug.Log("Landed, jump reset.");
        }
    }
}
