using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; // movement speed

    public float groundDrag; // ground drag value

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Keybinds")] // setting keys
    public KeyCode jumpKey = KeyCode.Space; // sets jump key to the spacebar. Look at MyInput Funtion for how we check if the key is pressed

    [Header("Ground Check")]
    public float playerHeight; //Player's height
    public LayerMask whatIsGround; //detcts what is the ground. Probably fro greyBoxing later

    bool grounded; // true of false value telling us if the player is on the ground or not. Well use raycasting for that

    public Transform orientation;   //the players orientation

    float horizontalInput; // horizolatal  keyboard inputs
    float verticalInput;    // vertical keyboard Inputs

    Vector3 moveDirection;

    Rigidbody rb; //Reference to the rigidbody

    private void Start()
    {
        //readyToJump = true; ;
        rb = GetComponent<Rigidbody>(); //Assign the rigidbody
        rb.freezeRotation = true; //This freeze the players rigit body so the player does fall over

    }

    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight *0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        //handle the drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput() //This is like the Mouse input funtion we did in the mouse movement stript. 
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded) //The key is pressed AND the player is on the ground AND they are ready to jump
        {
             
            Jump(); // Call the jump fuction
            readyToJump = false;
            Invoke(nameof(ResetJump), jumpCooldown); // the reset jump funtion is called with the jump cooldown as the delay.
        }
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput; // This makes is so that youre always walking in the direction you're looking

        // on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl() // This fucntion manualy keeps the players speed at the assigned value. The player keeps increasing in speed when this isn't doen. I donyk know why yet.
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //the find the value of the velocity

        // limits speed of needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed; // 
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); //
        }
    }

    private void Jump()
    {
        //Before you apply any forces you want to reset the rb's Y velocity to Zero. This is so that you alwasy jump the same hieght
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); // we use ForceMode Impusle because we're only multipliying the force once;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
