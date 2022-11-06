//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class ThirdPersonController : MonoBehaviour
//{
//    /* PRIVATE FIELDS */
//    private PlayerInputAction playerControls; // Reference Player Controller.
//    private InputAction move;
//    private Rigidbody rb;
//    private Animator anim;
//    private Vector3 forceDirection = Vector3.zero;
//    private bool isJumping;
//    private bool canJump;

//    /* SERIALIZED FIELDS */
//    [SerializeField] private Camera playerCamera;
//    [SerializeField]
//    private float movementForce;
//    [SerializeField]
//    private float jumpForce;
//    [SerializeField]
//    private float maxSpeed;

//    private void Awake()
//    {
//        anim = this.GetComponent<Animator>(); 
//        playerControls = new PlayerInputAction(); // Instantiate controlls.
//        move = playerControls.Land.Move;
//    }
//    private void OnEnable()
//    {
//        playerControls.Land.Enable(); // Enable Action Map Inputs.
//    }
//    /**********************************/
//    private void OnDisable()
//    {
//        playerControls.Land.Disable(); // Set up Action disable.
//    }
//    /**********************************/
//    private void FixedUpdate()
//    {
//        AddForceDirection();
//        PlayerJumpAndGravity();
//    }
//    /**********************************/
//    private void LookAt()
//    {
//        Vector3 direction = rb.velocity;
//        direction.y = 0.0f;
//        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
//        {
//            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
//        }
//        else
//        {
//            rb.angularVelocity = Vector3.zero;
//        }
//    }
//    /**********************************/
//    private void AddForceDirection()
//    {
//        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
//        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

//        rb.AddForce(forceDirection, ForceMode.Impulse);
//        forceDirection = Vector3.zero;

//        if(rb.velocity.y == 0.0f)
//        {
//            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
//        }

//        Vector3 horizontalVelocity = rb.velocity;
//        horizontalVelocity.y = 0;
//        if(horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
//        {
//            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
//        }

//        LookAt();
//    }
//    /**********************************/

        
//    /**********************************/
//    private void PlayerJump()
//    {
//        Debug.Log("JUMP AROUND! " + Vector3.up + " * " + jumpForce);
//        forceDirection += Vector3.up * jumpForce;
//    }
//    /**********************************/
//    private void PlayerJumpAndGravity()
//    {
//        /* Jumping / Gravity */
//        isJumping = playerControls.Land.JumpPressed.triggered;
//        Debug.Log("Is Jumping? " + isJumping);
//        // Apply Gravity if hero is not grounded.
//        if (IsGrounded() && isJumping)
//        {
//            //anim.SetBool("Grounded", false);
//            PlayerJump(); 
//        }
//        // anim.SetFloat("VertSpeed", ySpeed, 0.0f, Time.deltaTime);

//    }
//    /**********************************/
//    private bool IsGrounded()
//    {
//        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
//        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        } 
//    }
//    /**********************************/
//    private Vector3 GetCameraRight(Camera cam)
//    {
//        Vector3 right = playerCamera.transform.right;
//        right.y = 0;
//        return right.normalized;
//    }
//    /**********************************/
//    private Vector3 GetCameraForward(Camera cam)
//    {
//        Vector3 forward = playerCamera.transform.forward;
//        forward.y = 0;
//        return forward.normalized;
//    }
//}
