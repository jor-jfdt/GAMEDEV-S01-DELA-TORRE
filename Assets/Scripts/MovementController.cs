using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{

    [SerializeField]
    PlayerStats stats;

    Vector2 moveInput;

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float jumpHeight = 2f;

    [SerializeField]
    float gravity = -9.81f;

    float verticalVelocity;

    //[SerializeField]
    CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //Debug.Log(stats.Health);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && verticalVelocity < 0) {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        Vector3 movement = new Vector3(moveInput.x * moveSpeed, verticalVelocity, moveInput.y * moveSpeed);
        controller.Move(movement * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded) {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
