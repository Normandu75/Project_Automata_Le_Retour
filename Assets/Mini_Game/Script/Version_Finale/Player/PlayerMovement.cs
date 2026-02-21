using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    bool isGravityReversed;
    float cameraAngle = 0f;
    bool floor;
    bool ceiling;
    bool canMove;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        readyToJump = true;

        canMove = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, -transform.up, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();

        SpeedControl();

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            MovePlayer();            
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // when to reverse gravity
        if (Input.GetKeyDown(KeyCode.G))
        {
            //Enable gravity reversal
            ReverseGravity();
        }

        if (isGravityReversed == true)
        {
            // Rotate the camera to match the new gravity direction
            if (floor == true)
            {
                cameraAngle = Mathf.MoveTowards(cameraAngle, 180f, 120f * Time.deltaTime);

                transform.rotation = Quaternion.Euler(0f, 0f, cameraAngle);
            }

            // Rotate the camera back to normal when gravity is reversed again
            if(ceiling == true)
            {
                cameraAngle = Mathf.MoveTowards(cameraAngle, 0f, 120f * Time.deltaTime);

                transform.rotation = Quaternion.Euler(0f, 0f, cameraAngle);
            }
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;

            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        // reset jumping again after cooldown
        readyToJump = true;
    }

    private void ReverseGravity()
    {
        // Toggle gravity reversal
        isGravityReversed = true;

        // Disable player movement while gravity is reversed
        canMove = false;
    
        // Reverse the gravity direction
        Physics.gravity = -Physics.gravity;

        // Disable walls colliders while gravity is reversed
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            wall.GetComponent<BoxCollider>().enabled = false;
        }
    }

        void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Disable gravity reversal when player touches the floor
            isGravityReversed = false;

            // Enable player movement when player touches the floor
            canMove = true;

            // Set floor state
            floor = true;

            // Set ceiling state
            ceiling = false;

            // Enable wall colliders when player touches the floor
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (GameObject wall in walls)
            {
                wall.GetComponent<BoxCollider>().enabled = true;
            }
        }

        if (collision.gameObject.CompareTag("Ceiling"))
        {
            // Disable gravity reversal when player touches the ceiling
            isGravityReversed = false;

            // Enable player movement when player touches the ceiling
            canMove = true;

            // Set floor state
            floor = false;

            // Set ceiling state
            ceiling = true;

            // Enable wall colliders when player touches the ceiling
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (GameObject wall in walls)
            {
                wall.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}