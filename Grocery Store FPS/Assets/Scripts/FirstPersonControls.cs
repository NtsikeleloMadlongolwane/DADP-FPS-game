using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonControls : MonoBehaviour
{

    [Header("MOVEMENT SETTINGS")]
    [Space(5)]
    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed; // Speed at which the player moves
    public float lookSpeed; // Sensitivity of the camera movement
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 1.0f; // Height of the jump
    public Transform playerCamera; // Reference to the player's camera
                                   // Private variables to store input values and the character controller
    private Vector2 moveInput; // Stores the movement input from the player
    private Vector2 lookInput; // Stores the look input from the player
    private float verticalLookRotation = 0f; // Keeps track of vertical camera rotation for clamping
    private Vector3 velocity; // Velocity of the player
    private CharacterController characterController; // Reference to the CharacterController component

    [Header("SHOOTING SETTINGS")]
    [Space(5)]
    public GameObject projectilePrefab; // Projectile prefab for shooting
    public Transform RightFirePoint;
    // public Transform LeftFirePoint;

    // Point from which the projectile is fired
    public float projectileSpeed = 20f; // Speed at which the projectile is fired
    public float pickUpRange = 3f; // Range within which objects can be picked up
                                   // private bool holdingGun = false;

    [Header("PICKING UP SETTINGS")]
    [Space(5)]
    public Transform holdPosition; // Position where the picked-up object will be held
    private GameObject heldObject; // Reference to the currently held object


    // Crouch settings
    [Header("CROUCH SETTINGS")]
    [Space(5)]
    public float crouchHeight = 1f; // Height of the player when crouching
    public float standingHeight = 2f; // Height of the player when standing
    public float crouchSpeed = 2.5f; // Speed at which the player moves when crouching
    private bool isCrouching = false; // Whether the player is currently crouching

    [Header("INTERACT SETTINGS")]
    [Space(5)]
    public Material switchMaterial; // Material to apply when switch is activated
    public GameObject[] objectsToChangeColor; // Array of objects to change color

    [Header("CUSTOM MECHANICS")]

    public string objectName; // this checks the objcets name and unlockes certain unpgrades that conrelates with the object

    [Header("DoubleJump")]
    public bool doubleJumpUnlocked = false;
    public bool midAir = true; //double Jump Bool
    public float doubleJumpModifier = 0f; // Modifier. This is mutlipied with the jump height to get double jumo height
    [Space(5)]

    [Header("GunCrown")]
    public bool gunCrownUnlocked = true;
    public int Ammunition;
    public float shootingCooldown = 0f;

    [Header("Respawn")]
    public bool isRespawning = false;
    public Transform player;
    public Transform respawnPoint;

    [Header("UI")]
    public GameObject[] health;
    public GameObject[] heals;
    public int firstAid = 2;
    public PlayerHealth playerHealing;
    public ParticleSystem particleSystem;
    [Space(5)]
    public GameObject[] cursor;
    public GameObject[] playerPNG;
    public bool gamePaused = false;
    public ButtonHandler buttonHandler;
    [Space(5)]
    [Header("Intactacting with objects")]
    public float rayDistance = 1f; // Distance the ray will cast
    public string objectNameText;

    public GameObject actionUI;
    public TextMeshProUGUI objectText;
    public TextMeshProUGUI objectDiscription;
    public TextMeshProUGUI objectHowToUse;





    private void Awake()
    {
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        //objectText = GetComponent<TextMeshPro>();
       // objectDiscription = GetComponent<TextMeshPro>();
       // objectHowToUse = GetComponent<TextMeshPro>();

    }
    private void OnEnable()
    {
        // Create a new instance of the input actions
        var playerInput = new Controls();

        // Enable the input actions
        playerInput.Player.Enable();

        // Subscribe to the movement input events
        playerInput.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
        playerInput.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

        // Subscribe to the look input events
        playerInput.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>(); // Update lookInput when look input is performed
        playerInput.Player.Look.canceled += ctx => lookInput = Vector2.zero; // Reset lookInput when look input is canceled

        // Subscribe to the jump input event
        playerInput.Player.Jump.performed += ctx => Jump(); // Call the Jump method when jump input is performed

        // Subscribe to the shoot input event
        playerInput.Player.Shoot.performed += ctx => GunCrown(); // Call the Shoot method when shoot input is performed

        // playerInput.Player.Attack.performed += ctx => Attack(); // call attack when attack is performed

        // Subscribe to the pick-up input event
        playerInput.Player.PickUp.performed += ctx => PickUpObject(); // Call the PickUpObject method when pick-up input is performed

        // Subscribe to the crouch input event
        playerInput.Player.Crouch.performed += ctx => ToggleCrouch(); // Call the ToggleCrouch method when crouch input is performed

        // Subscribe to the interact input event
        playerInput.Player.Interact.performed += ctx => Interact(); // Interact with switch\

        playerInput.Player.Heal.performed += ctx => Heal(); // Interact with switch\

        playerInput.Player.PauseMenu.performed += ctx => PausetMenu(); // Interact with switch\


    }

    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation
        Move();
        LookAround();
        ApplyGravity();

        //UI
        CheckForPickUp();
    }

    public void Move()
    {
        // Create a movement vector based on the input
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // Transform direction from local to world space
        move = transform.TransformDirection(move);

        // Adjust speed if crouching
        float currentSpeed;
        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }


        // Move the character controller based on the movement vector and speed
        characterController.Move(move * currentSpeed * Time.deltaTime);
    }

    public void LookAround()
    {
        // Get horizontal and vertical look inputs and adjust based on sensitivity
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;

        // Horizontal rotation: Rotate the player object around the y-axis
        transform.Rotate(0, LookX, 0);

        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        // Apply the clamped vertical rotation to the player camera
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
    }

    public void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f; // Small value to keep the player grounded
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity
        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            midAir = true;
        }
        else
        {
            if (midAir == true)
            {
                DoubleJump();
                midAir = false;
            }
        }
    }

    /* public void Shoot()
     {
         if (holdingGun == true)
         {
             // Instantiate the projectile at the fire point
             GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

             // Get the Rigidbody component of the projectile and set its velocity
             Rigidbody rb = projectile.GetComponent<Rigidbody>();
             rb.velocity = firePoint.forward * projectileSpeed;

             // Destroy the projectile after 3 seconds
             Destroy(projectile, 3f);
         }
     }*/

    public void PickUpObject()
    {
        // Check if we are already holding an object
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics

            heldObject.GetComponent<RotateItems>().enabled = true; // set rotaetion on

            // heldObject.GetComponent<ParticleSystem>().Play();

            heldObject.transform.parent = null;
            // holdingGun = false;
        }

        // Perform a raycast from the camera's position forward
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 2f);


        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the hit object has the tag "PickUp"
            if (hit.collider.CompareTag("PickUp"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                heldObject.GetComponent<RotateItems>().enabled = false; // set rotaetion off

                // heldObject.GetComponent<ParticleSystem>().Stop();

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

                //UDGRADE CHECK
                objectName = heldObject.transform.name;
                Debug.Log(objectName);

                if (objectName == "Key 1")
                {
                    doubleJumpUnlocked = true;
                    Debug.Log("You just picked up " + objectName);
                }
                else if (objectName == "StarkKey")
                {
                    gunCrownUnlocked = true;
                    Debug.Log("You just picked up " + objectName);
                    playerPNG[0].SetActive(false);
                    playerPNG[1].SetActive(true);
                }

            }
            else if (hit.collider.CompareTag("Gun"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;

                // holdingGun = true;
            }
        }
    }

    public void ToggleCrouch()
    {
        if (isCrouching)
        {
            // Stand up
            characterController.height = standingHeight;
            isCrouching = false;
        }
        else
        {
            // Crouch down
            characterController.height = crouchHeight;
            isCrouching = true;
        }
    }

    public void Interact()
    {
        // Perform a raycast to detect the lightswitch
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            if (hit.collider.CompareTag("Switch")) // Assuming the switch has this tag
            {
                // Change the material color of the objects in the array
                foreach (GameObject obj in objectsToChangeColor)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = switchMaterial.color; // Set the color to match the switch material color
                    }
                }
            }

            else if (hit.collider.CompareTag("Door")) // Check if the object is a door
            {
                // Start moving the door upwards
                StartCoroutine(RaiseDoor(hit.collider.gameObject));
            }
            else if (hit.collider.CompareTag("Upgrade"))
            {
                gunCrownUnlocked = true;
                Debug.Log("You just picked up " + objectName);
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private IEnumerator RaiseDoor(GameObject door)
    {
        float raiseAmount = 5f; // The total distance the door will be raised
        float raiseSpeed = 2f; // The speed at which the door will be raised
        Vector3 startPosition = door.transform.position; // Store the initial position of the door
        Vector3 endPosition = startPosition + Vector3.up * raiseAmount; // Calculate the final position of the door after raising

        // Continue raising the door until it reaches the target height
        while (door.transform.position.y < endPosition.y)
        {
            // Move the door towards the target position at the specified speed
            door.transform.position = Vector3.MoveTowards(door.transform.position, endPosition, raiseSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame before continuing the loop
        }
    }


    /// CUSTOM CODE/
    public void DoubleJump()
    {
        if (doubleJumpUnlocked == true)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * doubleJumpModifier) * -2f * gravity);
        }
    }


    public void GunCrown()
    {
        if (gunCrownUnlocked == true)
        {
            if (Ammunition == 1)
            {

                // Instantiate the projectile at the fire point
                GameObject projectile1 = Instantiate(projectilePrefab, RightFirePoint.position, RightFirePoint.rotation);
                //GameObject projectile2 = Instantiate(projectilePrefab, LeftFirePoint.position, LeftFirePoint.rotation);

                // Get the Rigidbody component of the projectile and set its velocity
                Rigidbody rb = projectile1.GetComponent<Rigidbody>();
                rb.velocity = RightFirePoint.forward * projectileSpeed;

                // Rigidbody rb2 = projectile2.GetComponent<Rigidbody>();
                // rb2.velocity = LeftFirePoint.forward * projectileSpeed;

                gunCrownUnlocked = false;
                // Destroy the projectile after 3 seconds
                //Destroy(projectile1, 3f);
                //Destroy(projectile2, 3f);
                Invoke("ShootingCooldown", shootingCooldown);

            }

        }
    }
    void ShootingCooldown()
    {
        Debug.Log("This function is called after a 2-second delay.");
        gunCrownUnlocked = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    void Heal()
    {
        if (firstAid > 0)
        {
            playerHealing.Healing();
            playerHealing.TakeDamage(0);
            particleSystem.Play();
            firstAid--;
        }

        if (firstAid == 1)
        {
            heals[0].SetActive(true);
            heals[1].SetActive(false);
        }
        else
        {
            heals[0].SetActive(false);
            heals[1].SetActive(false);
        }

    }
    void PausetMenu()
    {
        if (gamePaused == false)
        {
            buttonHandler.PauseMenu();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            gamePaused = true;
            Time.timeScale = 0;
        }
        else
        {
            buttonHandler.Resume();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gamePaused = false;
            Time.timeScale = 1;
        }
    }

    private void CheckForPickUp()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (Physics.Raycast(ray, out hit, rayDistance))

            {
                if (hit.collider.CompareTag("PickUp"))
                {
                    actionUI.SetActive(true);
                    string objectName = hit.collider.gameObject.name;
                    string objectTag = hit.collider.gameObject.tag;
                    objectHowToUse.text = "PRESS [E] TO PICK UP";

                    if (objectName == "StarkKey")
                    {
                        objectText.text = "CRYSTAL KEY";
                        objectDiscription.text = "KEY THAT OPENS DOOR TO DOUNGOEN BOSS";
                    }
                    else if (objectName == "BlackKey")
                    {
                        objectText.text = "VOID KEY";
                        objectDiscription.text = "KEY THAT OPENS DOOR TO DOUNGOEN BOSS";
                    }


                    Debug.Log("Object Name: " + objectName + ", Object Tag: " + objectTag);
                }
                else if (hit.collider.CompareTag("Door"))
                {
                    actionUI.SetActive(true);
                    string objectName = hit.collider.gameObject.name;
                    string objectTag = hit.collider.gameObject.tag;

                    objectText.text = "DOOR";
                    objectDiscription.text = "";
                    objectHowToUse.text = "PRESS [F] TO OPEN";

                    Debug.Log("Object Name: " + objectName + ", Object Tag: " + objectTag);
                }
                else if (hit.collider.CompareTag("LockedDoor"))
                {
                    actionUI.SetActive(true);
                    string objectName = hit.collider.gameObject.name;
                    string objectTag = hit.collider.gameObject.tag;
                    objectHowToUse.text = "";

                    objectText.text = "LOCKED DOOR";

                    if (objectName == "Elevator Lock")
                    {
                        objectDiscription.text = "PLACE BOTH KEYS ON THE PODIUMS";
                    }
                    else if (objectName == "EnemyRoomLock")
                    {
                        objectDiscription.text = "PLACE CRYSTAL KEY ON THE CORRECT PODIUM";
                    }

                    Debug.Log("Object Name: " + objectName + ", Object Tag: " + objectTag);
                }
            }
            else
            {
                actionUI.SetActive(false);
                objectText.text = "";
                objectHowToUse.text = "";
                objectDiscription.text = "";
            }
        }
    }
}