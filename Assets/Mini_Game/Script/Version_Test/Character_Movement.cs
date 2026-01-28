using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class Character_Movement : MonoBehaviour
{
    [SerializeField]
    public float _movespeed = 5.0f;
    [SerializeField]
    public float _gravity = 9.81f;
    [SerializeField]
    public float _jumpspeed = 3.0f;
    [SerializeField]
    public float _DoubleJumpMultiplier = 3.0f;
    private CharacterController _controller;
    private float _directionY;
    private bool _CanDoubleJump = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalinput, 0, verticalinput);

        if (_controller.isGrounded)
        {
            _CanDoubleJump = true;
            
            if (Input.GetButtonDown("Jump"))
            {
                _directionY = _jumpspeed;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && _CanDoubleJump)
            {
                _directionY = _jumpspeed * _DoubleJumpMultiplier;
                _CanDoubleJump = false;
            }
        }

        _directionY -= _gravity * Time.deltaTime;

        direction.y = _directionY;

        _controller.Move(direction * _movespeed * Time.deltaTime);
    }
}