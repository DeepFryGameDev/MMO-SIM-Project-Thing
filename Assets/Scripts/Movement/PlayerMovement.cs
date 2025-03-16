using UnityEngine;

// Purpose: Provides the base moving the player around the world
// Directions: Attach to player GameObject
// Other notes: Written from YouTube tutorial: https://www.youtube.com/watch?v=f473C43s8nE

public class PlayerMovement : MonoBehaviour
{
    public bool movementEnabled;

    [Header("Movement")]
    [Tooltip("Base speed at which the player moves while sprinting")]
    [SerializeField] float sprintSpeed;

    [Tooltip("Additional drag to apply to the player while grounded")]
    [SerializeField] float groundDrag;

    [Tooltip("Any additional force to add while the player is in the air")]
    [SerializeField] float airMultiplier;

    [Header("Keybinds")]
    [Tooltip("Key to hold to sprint")]
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    //[Tooltip("Key to press to jump (not being used right now)")]
    //[SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    [Tooltip("Raycast is shot downward from the player's height to ensure they are grounded")]
    [SerializeField] float playerHeight;
    [Tooltip("Layer of ground")]
    [SerializeField] LayerMask whatIsGround;
    [Tooltip("Turns true when player is not in the air")]
    bool grounded;

    Transform orientation; // Set to the Player -> Orientation Transform during SetVars()

    float moveSpeedModifier = 10f; // Speed modifier used during calculation to determine how fast player should move

    float combatCameraMovingBackwardsSpeedModifier = 5f; // Speed modifier used during calculation to determine how fast player should move when moving backwards during combat

    float horizontalInput; // Set to any movement from the player along the X axis
    float verticalInput; // Set to any movement from the player along the Y axis

    Vector3 moveDirection; // Direction the player is moving towards

    BaseHero player; // Used to gather the player's stamina levels
    PlayerManager pm; // Used to manipulate stamina when player sprints

    Rigidbody rb; // Used to add force to the GameObject so the player is able to move
    Animator anim; // Used to change the player's animations while walking and sprinting

    CameraManager cm; // Used to get the current camera mode so user input can be provided to the player's animation.  Also used to disable sprinting while moving backwards in combat mode

    Collider col; // Used to determine the distance to the ground via raycast

    bool movementSet; // Set to true when all movement variables have been set for a game startup

    static PlayerMovement instance; // For singleton to ensure script persists across scenes

    // -- Disabled for now - jumping is not active
    //public float jumpForce;
    //public float jumpCooldown;
    // bool readyToJump;

    void Awake()
    {
        //Singleton();
        movementEnabled = true;
    }

    void Singleton()
    {
        if (instance == null) //check if instance exists
        {
            instance = this; //if not set the instance to this
        }
        else if (instance != this) //if it exists but is not this instance
        {
            Destroy(gameObject); //destroy it
        }
        DontDestroyOnLoad(gameObject); //set this to be persistable across scenes
    }

    void FixedUpdate()
    {
        if (movementSet)
        {
            MovePlayer();
        }
        
    }

    void Update()
    {
        if (movementSet && movementEnabled)
        {
            // ground check
            //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .1f, whatIsGround);
            grounded = Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + .1f, whatIsGround);

            GetInput();
            SpeedControl();

            HandleAnimations();

            // handle drag
            if (grounded)
            {
                //Debug.Log("Grounded");
                rb.linearDamping = groundDrag;
            }
            else
            {
                //Debug.Log("Not grounded");
                rb.linearDamping = 0;
            }            
        }
    }

    /// <summary>
    /// Turns movement for the player on or off.  Will also toggle player's rigidbody isKinematic.
    /// </summary>
    /// <param name="toggle">True to enable movement, false to disable movement.</param>
    public void ToggleMovement (bool toggle)
    {
        movementEnabled = toggle;

        rb.isKinematic = !toggle;
    }

    /// <summary>
    /// Sets the vars needed for game startup
    /// </summary>
    public void SetVars(GameObject playerParent)
    {
        pm = GetComponent<PlayerManager>();

        cm = FindFirstObjectByType<CameraManager>();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        anim = playerParent.GetComponent<Animator>();

        // readyToJump = true;

        col = playerParent.GetComponentInChildren<Collider>();

        orientation = playerParent.transform.Find("Orientation");

        movementSet = true;
    }

    /// <summary>
    /// Gather the player's input for movement and process animations/sprinting mechanisms
    /// </summary>
    void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (Input.GetKey(sprintKey) && !IsMovingBackwards())
            {
                //nothing for now
            } else
            { 
                //nothing for now
            }
        } else
        {
            
        }

        // when to jump
        /*if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }*/
    }

    /// <summary>
    /// The animator attached to the player will have bools changed based on player's movement input
    /// </summary>
    void HandleAnimations()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (Input.GetKey(sprintKey))
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isSprinting", true);
            }
            else
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isSprinting", false);
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isSprinting", false);
        }   
    }

    /// <summary>
    /// Uses the attached RigidBody to move the player around the world
    /// BASIC camera mode is one speed, but COMBAT is slower when moving backwards
    /// </summary>
    void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        float speedModifier = 0;

        // on ground
        if (grounded)
        {
            switch (cm.currentMode) // Sets speed modifier for moving
            {
                case EnumHandler.CameraModes.BASIC:
                    speedModifier = moveSpeedModifier;
                    break;
                case EnumHandler.CameraModes.COMBAT:
                    if (!IsMovingBackwards())
                    {
                        speedModifier = moveSpeedModifier;

                        //Debug.Log("Normal move speed: " + finalSpeed);
                    }
                    else
                    {
                        //Debug.Log("move at half speed");
                        speedModifier = combatCameraMovingBackwardsSpeedModifier;

                        //Debug.Log("Half move speed: " + finalSpeed);
                    }
                    break;
            }

            if (Input.GetKey(sprintKey) && !IsMovingBackwards()) // Player is sprinting
            {
                rb.AddForce(moveDirection.normalized * (sprintSpeed * speedModifier), ForceMode.Force);
            }
            else // Player is walking
            {
                rb.AddForce(moveDirection.normalized * (pm.GetMoveSpeed() * speedModifier), ForceMode.Force);
            }
        }

        // in air
        else if (!grounded)
        {
            if (Input.GetKey(sprintKey) && !IsMovingBackwards())
            {
                rb.AddForce(moveDirection.normalized * sprintSpeed * moveSpeedModifier * airMultiplier, ForceMode.Force);
            } else
            {
                rb.AddForce(moveDirection.normalized * pm.GetMoveSpeed() * moveSpeedModifier * airMultiplier, ForceMode.Force);
            }
        }
    }

    /// <summary>
    /// Simply checks if the player is moving backwards
    /// </summary>
    /// <returns>True if vertical input is < 0. False if >= 0.</returns>
    bool IsMovingBackwards()
    {
        if (verticalInput < 0 )
        {
            return true;
        } else
        {
            return false;
        }        
    }

    /// <summary>
    /// Ensures the rigidbody's velocity does not exceed moveSpeed
    /// </summary>
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > pm.GetMoveSpeed())
        {
            Vector3 limitedVel = flatVel.normalized * pm.GetMoveSpeed();
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

        /*if (Input.GetAxisRaw("Horizontal").Equals(0) && Input.GetAxisRaw("Vertical").Equals(0) && !rb.isKinematic && grounded)
        {
            rb.isKinematic = true;
        } else
        {
            rb.isKinematic = false;
        }*/
    }

    /*void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }*/
}
