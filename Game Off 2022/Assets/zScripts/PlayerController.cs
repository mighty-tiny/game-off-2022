//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerController : MonoBehaviour
//{
    
//    /* PLAYER */
//    private PlayerInputAction playerControls; // Reference Player Controller.
//    private CharacterController characterController; // Reference CharacterController
//    private Animator anim;
//    private Vector3 velocity;
//    private float horizontalInput;
//    private float verticalInput;
//    private float ySpeed;
//    private bool isSprinting = false;
//    private bool isHit = false;
//    private Vector2 move;

//    /* SERIALIZED */
//    [SerializeField] private GameObject player;
//    [SerializeField] LayerMask groundLayers;
//    [SerializeField] private Transform[] groundChecks;
//    [SerializeField] private int maxPlayerLives;
//    [SerializeField] private int minPlayerLives;
//    [SerializeField] private int playerLives;
//    [SerializeField] private int maxPlayerHealth;
//    [SerializeField] private int minPlayerHealth;
//    [SerializeField] private int playerHealth;
//    [SerializeField] private float movementSpeed = 0;
//    [SerializeField] private float walkSpeed; // Walk Speed
//    [SerializeField] private float runSpeed; // Run Speed
//    [SerializeField] private float bounceSpeed; // Speed at which player bounces away from enemies.
//    [SerializeField] private float bounceTime; // How long will a player bounce away when hit?
//    [SerializeField] private float jumpHeight; // Jump Height

//    /* GRAVITY */
//    [SerializeField] private float gravity = -50f;
//    private bool isGrounded;
//    private bool isJumping;

//    private void Awake()
//    {
//        // Player Controls
//        playerControls = new PlayerInputAction(); // Instantiate controlls.

//        // Sprint
//        playerControls.Land.SprintStart.performed += x => SprintPressed();
//        playerControls.Land.SprintEnd.performed += x => SprintReleased();
//    }
//    private void OnEnable()
//    {
//        playerControls.Enable(); // Enable Action Map Inputs.
//    }

//    private void OnDisable() 
//    {
//        playerControls.Disable(); // Set up Action disable.
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        characterController = GetComponent<CharacterController>();
//        // anim = GetComponentInChildren<Animator>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        GroundCheck();
//        playerJumpandGravity();
//        if (!isHit)
//        {
//            PlayerMovement();
//        }
//        else
//        {
//            PlayerBounce();
//        }
//    }

//    /* PLAYER MOVEMENT FUNCTIONS START */
//    private void PlayerMovement()
//    {
//        // Movement
//        move = playerControls.Land.Move.ReadValue<Vector2>();
//        horizontalInput = move.y;
//        verticalInput = move.x;

//        // GoingRight();
//        Vector3 movementDirection = new Vector3(verticalInput, ySpeed, horizontalInput);

//        // Speed?
//        if (isGrounded)
//        {
//            if (movementDirection != Vector3.zero)
//            {
//                if (!isSprinting)
//                {
//                    // Walk
//                    Walk();
//                }
//                else if (isSprinting)
//                {
//                    // Run
//                    Run();
//                }
//            }
//            else if (movementDirection == Vector3.zero)
//            {
//                // Idle
//                Idle();
//            }
//        }

//        movementDirection.Normalize();
       
//        if (movementDirection != Vector3.zero)
//        {
//            transform.forward = movementDirection;
//        }

//        velocity = movementDirection * movementSpeed;
//        velocity.y = ySpeed;
//        // Debug.Log("Velocity? " + velocity);
//        characterController.Move(velocity * Time.deltaTime);
//    }
//    /* PLAYER MOVEMENT FUNCTIONS END */

//    /* GRAVITY AND JUMP FUNCTIONS  START */
//    private void GroundCheck()
//    {
//        isGrounded = false;
//        foreach (var check in groundChecks)
//        {
//            // Is the player touching the ground?
//            if (Physics.CheckSphere(transform.position, 0.25f, groundLayers, QueryTriggerInteraction.Ignore))
//            {
//                isGrounded = true;
//                break;
//            }
//        }
        
//    }
//    private void playerJumpandGravity()
//    {
//        /* Jumping / Gravity */
//        isJumping = playerControls.Land.JumpPressed.triggered;

//        // Apply Gravity if hero is not grounded.
//        if (isGrounded && isJumping)
//        {
//            // anim.SetBool("Grounded", false);
//            ySpeed += Mathf.Sqrt(jumpHeight * -2 * gravity); // 10 * -2 * -50   
//        }
//        else if (isGrounded)
//        {
//            // anim.SetBool("Grounded", true);
//            ySpeed = velocity.y = 0.0f;
//        }
//        else
//        {
//            ySpeed += gravity * Time.deltaTime;
//            if (ySpeed < -20)
//            {
//                ySpeed = -20;
//            }
//            // Debug.Log("GRAVITY: " + ySpeed);
//        }
//        // anim.SetFloat("VertSpeed", ySpeed, 0.0f, Time.deltaTime);

//    }
//    /* PLAYER GRAVITY AND JUMP FUNCTIONS END */

//    /* PLAYER HIT FUNCTIONS START */
//    void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Enemy"))
//        {
//            isHit = true;
//            // GameManager.Instance.Score += pointValue;
//            // PlayerHit();
//        }
//    }

//    private void PlayerHit()
//    {
//        if (isHit)
//        {
//            StartCoroutine(HitReaction());
//        }
//    }

//    private IEnumerator HitReaction()
//    {
//        yield return new WaitForSeconds(bounceTime);
//        isHit = false;
//        // anim.SetBool("Hit", false);
//    }

//    private void PlayerBounce()
//    {
//        /*
//        if (goingRight)
//        {
//            horizontalInput = -1;
//        }
//        else if (!goingRight)
//        {
//            horizontalInput = 1;
//        }
        
//        Vector3 movementDirection = new Vector3(0.0f, 0.0f, horizontalInput);
//        velocity = movementDirection * bounceSpeed;
//        velocity.y = ySpeed;
//        characterController.Move(velocity * Time.deltaTime);
//        */
//    }
//    /* PLAYER HIT FUNCTIONS END */

//    /* WALKING AND RUNNING FUNCTIONS START */
//    private void SprintPressed()
//    {
//        isSprinting = true;
//    }

//    private void SprintReleased()
//    {
//        isSprinting = false;
//    }

//    private void Idle()
//    {
//        // anim.SetFloat("Speed", 0.0f, 0.1f, Time.deltaTime);

//    }

//    private void Walk()
//    {
//        movementSpeed = walkSpeed;
//        // anim.SetFloat("Speed", 1.0f, 0.1f, Time.deltaTime);
//    }

//    private void Run()
//    {
//        movementSpeed = runSpeed;
//        // anim.SetFloat("Speed", 2.0f, 0.1f, Time.deltaTime);
//    }
//    /* WALKING AND RUNNING FUNCTIONS END */
//}
