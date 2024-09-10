using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float turnSpeed = 200f;

    PlayerControls playerControls;
    Rigidbody2D rb;

    private float moveInput;
    private float turnInput;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void ProcessInputs()
    {
        Vector2 movement = playerControls.Movement.Move.ReadValue<Vector2>();
        moveInput = movement.y;
        turnInput = movement.x;
    }

    private void Move()
    {
        Vector2 forwardMove = transform.up * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);
    }

    private void Turn()
    {
        float turnAmount = -turnInput * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + turnAmount);
    }
}
