using System;
using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;

    [SerializeField] private Transform orientation;
    public MovementState MovementState;

    [Header("Movement")] public float MoveSpeed;
    public float WalkSpeed;
    public float SprintSpeed;
    public float CrouchSpeed;
    public float CrawlSpeed;

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private Vector2 moveInput;
    private Vector3 moveDirection;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDrag;
    [SerializeField] private bool grounded;

    [SerializeField] private float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [SerializeField] private float breakSpeedSmooth;
    [SerializeField] private float angle;

    [SerializeField] private PhysicMaterial playerBodyNormalPhysicMaterial;
    [SerializeField] private PhysicMaterial playerBodyFrictionPhysicMaterial;

    [Header("Jumping")] [SerializeField] private float jumpCooldown;
    [SerializeField] private float jumpForce;
    [SerializeField] private float airSpeed;
    private bool readyToJump = true;

    [Header("Crouch Crawl")] private bool crouching;
    private bool crawling;
    private CapsuleCollider capsuleCollider;
    private float targetHeight = 2f;
    private Vector3 targetPosition;
    [SerializeField] private float crouchHeightSmooth;
    [SerializeField] private float crawlHeightSmooth;


    private void Start()
    {
        if (!isLocalPlayer) return;
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        playerInputManager.PlayerInput.PlayerMovement.Crouch.performed += crouch;
        playerInputManager.PlayerInput.PlayerMovement.Crawl.performed += crawl;
    }


    private void Update()
    {
        if (!isLocalPlayer) return;
        setDrag();
        speedControl();
        rb.mass = angle > maxSlopeAngle ? 20 : 2;
        smoothColliders();
        stateHandler();
        grounded = Physics.CheckBox(groundCheck.position, new Vector3(.25f, .05f, .25f), Quaternion.identity, groundMask);
        if (MenuManager.Instance.Opened) return;
        getInput();
        jump();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        movePlayer();

        if (MovementState == MovementState.air) rb.AddForce(Vector3.down * 15, ForceMode.Force);
    }

    /// <summary>
    /// Smooth coliders height and moves ground check transform
    /// </summary>
    void smoothColliders()
    {
        switch (MovementState)
        {
            case MovementState.crouching:
                capsuleCollider.height = Mathf.Lerp(capsuleCollider.height, targetHeight, crouchHeightSmooth * Time.deltaTime);
                groundCheck.localPosition = Vector3.Lerp(groundCheck.localPosition, targetPosition, crouchHeightSmooth * Time.deltaTime);
                break;
            case MovementState.crawling:
                capsuleCollider.height = Mathf.Lerp(capsuleCollider.height, targetHeight, crawlHeightSmooth * Time.deltaTime);
                groundCheck.localPosition = Vector3.Lerp(groundCheck.localPosition, targetPosition, crawlHeightSmooth * Time.deltaTime);
                break;
            default:
                capsuleCollider.height = Mathf.Lerp(capsuleCollider.height, targetHeight, crouchHeightSmooth * Time.deltaTime);
                groundCheck.localPosition = Vector3.Lerp(groundCheck.localPosition, targetPosition, crouchHeightSmooth * Time.deltaTime);
                break;
        }
    }

    /// <summary>
    /// Resets movement states
    /// </summary>
    public void ResetStates()
    {
        MovementState = MovementState.walking;
        MoveSpeed = WalkSpeed;
        targetHeight = 2f;
        targetPosition = Vector3.zero;
        capsuleCollider.radius = .5f;
        crouching = false;
        crawling = false;
    }

    /// <summary>
    /// Gets movement input
    /// </summary>
    void getInput()
    {
        moveInput = playerInputManager.PlayerInput.PlayerMovement.Movement.ReadValue<Vector2>();
        horizontalInput = moveInput.x;
        verticalInput = moveInput.y;
    }

    /// <summary>
    /// Handles player move state
    /// </summary>
    void stateHandler()
    {
        //Idle
        if (moveInput == Vector2.zero && grounded && !crouching && !crawling)
        {
            MovementState = MovementState.idle;
        }
        //Sprinting
        else if (grounded && playerInputManager.PlayerInput.PlayerMovement.Sprint.IsPressed() && !crouching && !crawling && moveInput != Vector2.zero)
        {
            MovementState = MovementState.sprinting;
            MoveSpeed = SprintSpeed;
        }
        //Walking
        else if (grounded && !crouching && !crawling && moveInput != Vector2.zero)
        {
            MovementState = MovementState.walking;
            MoveSpeed = WalkSpeed;
        }
        //Crouching
        else if (crouching && grounded)
        {
            MovementState = MovementState.crouching;
        }
        //Crawling
        else if (crawling && grounded)
        {
            MovementState = MovementState.crawling;
        }
        //In air
        else if (!grounded)
        {
            MovementState = MovementState.air;
        }
    }

    /// <summary>
    /// Moves player
    /// </summary>
    void movePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (onSlope() && !exitingSlope)
        {
            rb.AddForce(getSlopeMoveDirection() * (MoveSpeed * 10f), ForceMode.Force);
        }
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * (MoveSpeed * 10f), ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * (MoveSpeed * 10f * airSpeed), ForceMode.Force);
        }

        if (onGravitySlope() && moveInput == Vector2.zero)
        {
            capsuleCollider.material = playerBodyFrictionPhysicMaterial;
        }
        else
        {
            capsuleCollider.material = playerBodyNormalPhysicMaterial;
        }
    }

    /// <summary>
    /// Sets rigidbody drag
    /// </summary>
    void setDrag()
    {
        if (!grounded)
        {
            rb.drag = 0;
        }
        else if (onGravitySlope() && !exitingSlope)
        {
            rb.drag = groundDrag;
        }
        else if (grounded)
        {
            rb.drag = groundDrag;
        }
    }

    /// <summary>
    /// Limits player speed
    /// </summary>
    private void speedControl()
    {
        if (MovementState == MovementState.idle)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(0, rb.velocity.y, 0), breakSpeedSmooth * Time.deltaTime);
        }

        if (onSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > MoveSpeed) rb.velocity = rb.velocity.normalized * MoveSpeed;
        }
        else
        {
            Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);
            if (!(flatVel.magnitude > MoveSpeed)) return;
            Vector3 limitedVel = flatVel.normalized * MoveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    /// <summary>
    /// Jump
    /// </summary>
    void jump()
    {
        if (!playerInputManager.PlayerInput.PlayerMovement.Jump.IsPressed()) return;
        if (!readyToJump || !grounded) return;
        if (angle > maxSlopeAngle) return;

        if (MovementState == MovementState.crawling || MovementState == MovementState.crouching)
        {
            crouching = false;
            crawling = false;
            targetHeight = 2f;
            capsuleCollider.radius = .5f;
            targetPosition = Vector3.zero;

            return;
        }

        exitingSlope = true;
        readyToJump = false;
        Invoke(nameof(resetJump), jumpCooldown);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Resets jump cooldown
    /// </summary>
    void resetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    /// <summary>
    /// Crouch
    /// </summary>
    void crouch(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        capsuleCollider.radius = .5f;
        crawling = false;
        crouching = !crouching;
        if (crouching)
        {
            targetHeight = 1f;
            targetPosition = new Vector3(0, .5f, 0);
            //rb.AddForce(Vector3.down * 100f, ForceMode.Impulse);
            MoveSpeed = CrouchSpeed;
        }
        else
        {
            targetPosition = Vector3.zero;
            targetHeight = 2f;
        }
    }

    /// <summary>
    /// Crawl
    /// </summary>
    void crawl(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        crouching = false;
        crawling = !crawling;
        if (crawling)
        {
            targetHeight = .5f;
            targetPosition = new Vector3(0, .75f, 0);
            //rb.AddForce(Vector3.down * 100f, ForceMode.Impulse);
            MoveSpeed = CrawlSpeed;
            capsuleCollider.radius = .25f;
        }
        else
        {
            targetPosition = Vector3.zero;
            targetHeight = 2f;
            capsuleCollider.radius = .5f;
        }
    }

    /// <summary>
    /// Checks slope under player
    /// </summary>
    bool onSlope()
    {
        if (!Physics.Raycast(groundCheck.position, Vector3.down, out slopeHit, 1f)) return false;
        angle = Vector3.Angle(Vector3.up, slopeHit.normal);
        return angle < maxSlopeAngle && angle != 0;
    }

    /// <summary>
    /// Checks slope under player for gravity toggling
    /// </summary>
    bool onGravitySlope()
    {
        if (!Physics.Raycast(groundCheck.position, Vector3.down, out slopeHit, .25f)) return false;
        float gravityAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
        return gravityAngle < maxSlopeAngle && gravityAngle != 0;
    }

    /// <summary>
    /// Returns slope direction
    /// </summary>
    Vector3 getSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}