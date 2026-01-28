using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementTutorial : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.G))
        {
            ReverseGravity();
        }

        if (isGravityReversed == true)
        {
            if (floor == true)
            {
                cameraAngle = Mathf.MoveTowards(cameraAngle, 180f, 120f * Time.deltaTime);

                //Debug.Log(cameraAngle);

                transform.rotation = Quaternion.Euler(0f, 0f, cameraAngle);
            }

            if(ceiling == true)
            {
                cameraAngle = Mathf.MoveTowards(cameraAngle, 0f, 120f * Time.deltaTime);

                //Debug.Log(cameraAngle);

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
        readyToJump = true;
    }

    private void ReverseGravity()
    {
        isGravityReversed = true;

        canMove = false;
    
        Physics.gravity = -Physics.gravity;
    }

        void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGravityReversed = false;

            canMove = true;

            floor = true;

            Debug.Log(floor);

            ceiling = false;

            Debug.Log(ceiling);
        }

        if (collision.gameObject.CompareTag("Ceiling"))
        {
            isGravityReversed = false;

            canMove = true;

            floor = false;

            Debug.Log(floor);

            ceiling = true;

            Debug.Log(ceiling);
        }
    }
}