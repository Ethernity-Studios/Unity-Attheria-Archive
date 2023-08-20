using System;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;
    
    [SerializeField] private Transform orientation;
    
    public float MoveSpeed;
    
    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDrag;
    private bool grounded;

    [SerializeField] private float jumpCooldown;
    [SerializeField] private float jumpForce;
    [SerializeField] private float airSpeed;
    private bool readyToJump = true;

    private void Start()
    {
        //if (!isLocalPlayer) return;
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        //if (!isLocalPlayer) return;
        if (MenuManager.Instance.Opened) return;

        grounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1.2f, groundMask);
        speedControl();
        getInput();
        jump();
    }

    private void FixedUpdate()
    {
        //if (!isLocalPlayer) return;
        movePlayer();
    }

    void getInput()
    {
        horizontalInput = playerInputManager.PlayerInput.PlayerMovement.Movement.ReadValue<Vector2>().x;
        verticalInput = playerInputManager.PlayerInput.PlayerMovement.Movement.ReadValue<Vector2>().y;
    }

    #region Movement

    void movePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.drag = groundDrag;
            rb.AddForce(moveDirection.normalized * (MoveSpeed * 10f), ForceMode.Force);
        }
        else
        {
            rb.drag = groundDrag / 2;
            rb.AddForce(moveDirection.normalized * (MoveSpeed * 10f * airSpeed), ForceMode.Force);
        }
    }

    private void speedControl()
    {
        Vector3 flatVel = new(rb.velocity.x, 0f, rb.velocity.z);
        if (!(flatVel.magnitude > MoveSpeed)) return;
        Vector3 limitedVel = flatVel.normalized * MoveSpeed;
        rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
    }

    #endregion


    #region Jump

    void jump()
    {
        if (!playerInputManager.PlayerInput.PlayerMovement.Jump.IsPressed()) return;
        if (!readyToJump || !grounded) return;
        readyToJump = false;
        Invoke(nameof(resetJump), jumpCooldown);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    void resetJump()
    {
        readyToJump = true;
    }

    #endregion

}
