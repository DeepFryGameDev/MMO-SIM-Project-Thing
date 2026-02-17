using UnityEngine;

// Purpose: Provides the base moving the player around the world
// Directions: Attach to player GameObject
// Other notes: Written from YouTube tutorial: https://www.youtube.com/watch?v=f473C43s8nE

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Toggles whether player movement is enabled or not.")]
    [SerializeField] bool movementEnabled;

    [Header("Movement")]
    [Tooltip("Base speed at which the player moves while sprinting")]
    [SerializeField] float sprintSpeed;

    [Tooltip("Additional drag to apply to the player while grounded")]
    [SerializeField] float groundDrag;

    [Tooltip("Any additional force to add while the player is in the air")]
    [SerializeField] float airMultiplier;

    [Header("Ground Check")]
    [Tooltip("Raycast is shot downward from the player's height to ensure they are grounded")]
    [SerializeField] float playerHeight;
    [Tooltip("Layer of ground")]
    [SerializeField] LayerMask whatIsGround;
    [Tooltip("Turns true when player is not in the air")]
    bool grounded;

    Transform orientation; // Set to the Player -> Orientation Transform during SetVars()

    float moveSpeedModifier = 10f; // Speed modifier used during calculation to determine how fast player should move

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

    [SerializeField] InputSubscription inputSubscription; // update this to grab the object from System object instead of being set in the inspector

    public static PlayerMovement i; // Instance of this script that can be called from other scripts via PlayerMovement.i

    // ------------------

    void Awake()
    {
        Singleton();

        movementEnabled = true;
    }

    void Singleton()
    {
        if (i == null) //check if instance exists
        {
            i = this; //if not set the instance to this
        }
        else if (i != this) //if it exists but is not this instance
        {
            Destroy(gameObject); //destroy it
        }
        DontDestroyOnLoad(gameObject); //set this to be persistable across scenes
    }

    void Update()
    {
        if (movementSet && movementEnabled)
        {
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

        //--------------------------------
    }

    void FixedUpdate()
    {
        if (movementSet && movementEnabled)
        {
            MovePlayer();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("whatIsGround"))
        {
            Debug.Log("Grounded");
            grounded = true;
        }
    }

    /// <summary>
    /// Turns movement for the player on or off.  Will also toggle player's rigidbody isKinematic.
    /// </summary>
    /// <param name="toggle">True to enable movement, false to disable movement.</param>
    public void ToggleMovement(bool toggle)
    {
        DebugManager.i.PlayerDebugOut("PlayerMovement", "Movement toggled: " + toggle);
        movementEnabled = toggle;

        if (rb == null) rb = GetComponent<Rigidbody>();
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

        //col = playerParent.GetComponentInChildren<Collider>();
        col = GetComponent<Collider>();

        orientation = playerParent.transform.Find("Orientation");

        movementSet = true;
    }

    /// <summary>
    /// Gather the player's input for movement and process animations/sprinting mechanisms
    /// </summary>
    void GetInput()
    {
        //horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");

        // set correct vars here:
        horizontalInput = inputSubscription.moveInput.x;
        verticalInput = inputSubscription.moveInput.z;

        //DebugManager.i.SystemDebugOut("PlayerMovement", "horizontalInput: " + horizontalInput + " / verticalInput: " + verticalInput);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (inputSubscription.sprintInput)
            {
                //nothing for now
            }
            else
            {
                //nothing for now
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero; // Keeps player object from sliding around after movement
        }
    }

    /// <summary>
    /// The animator attached to the player will have bools changed based on player's movement input
    /// </summary>
    void HandleAnimations()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            if (inputSubscription.sprintInput)
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
            }

            if (inputSubscription.sprintInput) // Player is sprinting
            {
                rb.AddForce(moveDirection.normalized * (sprintSpeed * speedModifier), ForceMode.Force);
                //Debug.Log("Sprinting: " + "moveDir: " + moveDirection + " / sprintSpeed: " + sprintSpeed + " / speedModifier: " + speedModifier);
            }
            else // Player is walking
            {
                rb.AddForce(moveDirection.normalized * (pm.GetMoveSpeed() * speedModifier), ForceMode.Force);
                //if (moveDirection != Vector3.zero) Debug.Log("Walking: moveDir: " + moveDirection + " / GetMoveSpeed: " + pm.GetMoveSpeed() + " / speedModifier: " + speedModifier);
            }
        }

        // in air
        else if (!grounded)
        {
            if (inputSubscription.sprintInput)
            {
                rb.AddForce(moveDirection.normalized * sprintSpeed * moveSpeedModifier * airMultiplier, ForceMode.Force);
                //Debug.Log("We're sprinting in the air! moveDir: " + moveDirection + " / GetMoveSpeed: " + pm.GetMoveSpeed() + " / speedModifier: " + moveSpeedModifier * airMultiplier);
            }
            else
            {
                rb.AddForce(moveDirection.normalized * pm.GetMoveSpeed() * moveSpeedModifier * airMultiplier, ForceMode.Force);
                //Debug.Log("We're in the air! moveDir: " + moveDirection + " / GetMoveSpeed: " + pm.GetMoveSpeed() + " / speedModifier: " + moveSpeedModifier * airMultiplier);
            }
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
}
