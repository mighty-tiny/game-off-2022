using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayerController : MonoBehaviour
{
    /* PLAYER */
    private PlayerInputAction playerControls; // Reference Player Controller.
    private CharacterController characterController; // Reference CharacterController
    private Animator anim;
    private Vector2 move;
    private Vector3 velocity;
    private float horizontalInput;
    private float verticalInput;
    private float ySpeed;
    private bool isSprinting;

    /* SERIALIZED */
    // [SerializeField] private GameObject player;
    // [SerializeField] private Transform cameraTransform; // Camera Rotation
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private Transform[] groundChecks;
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float walkSpeed; // Walk Speed
    [SerializeField] private float runSpeed; // Run Speed
    [SerializeField] private float bounceSpeed; // Speed at which player bounces away from enemies.
    [SerializeField] private float bounceTime; // How long will a player bounce away when hit?
    [SerializeField] private float jumpHeight; // Jump Height

    /* GRAVITY */
    [SerializeField] private float gravity = -50f;
    private bool isGrounded;
    private bool isJumping;

    private void Awake()
    {
        characterController = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        playerControls = new PlayerInputAction();
        /* Sprint */
        playerControls.Land.SprintPressed.performed += x => SprintPressed();
        playerControls.Land.SprintReleased.performed += x => SprintReleased();
    }
    /**********************************/
    private void OnEnable()
    {
        playerControls.Land.Enable();
    }
    /**********************************/
    private void OnDisable()
    {
        playerControls.Land.Disable();
    }
    /**********************************/
    private void Update()
    {
        PlayerJumpandGravity();
        PlayerMovement();
    }
    private void PlayerMovement()
    {
        // Movement
        move = playerControls.Land.Move.ReadValue<Vector2>();
        horizontalInput = move.x;
        verticalInput = move.y;

        Vector3 movementDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        // Speed?
        if (isGrounded)
        {
            if (movementDirection != Vector3.zero)
            {
                if (!isSprinting)
                {
                    // Walk
                    Walk();
                }
                else if (isSprinting)
                {
                    // Run
                    Run();
                }
            }
            else if (movementDirection == Vector3.zero)
            {
                // Idle
                Idle();
            }
        }
        // movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        velocity = movementDirection * movementSpeed;
        velocity.y = ySpeed;

        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }

        characterController.Move(velocity * Time.deltaTime);
    }
    private void PlayerJumpandGravity()
    {
        /* Jumping / Gravity */
        // Is the player touching the ground?
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore);
        isJumping = playerControls.Land.JumpPressed.triggered;

        // Apply Gravity if hero is not grounded.
        if (isGrounded && isJumping)
        {
            anim.SetBool("Grounded", false);
            ySpeed += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        else if (isGrounded)
        {
            anim.SetBool("Grounded", true);
            // velocity.y = 0.0f;
            ySpeed = velocity.y = 0.0f;
        }
        else
        {
            // velocity.y += gravity * Time.deltaTime;
            ySpeed += gravity * Time.deltaTime;
            if (ySpeed < -20)
            {
                ySpeed = -20;
            }
        }
        anim.SetFloat("VertSpeed", ySpeed, 0.0f, Time.deltaTime);
    }
    private void Idle() 
    { 
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime); 
    }
    private void Walk()
    {
        movementSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    private void Run()
    {
        movementSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }
    private void SprintPressed() { isSprinting = true; }
    private void SprintReleased() { isSprinting = false; }
    
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}