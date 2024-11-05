using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 16f;
    [SerializeField] private float turnSpeed = 200f;
    [SerializeField] private ParticleSystem exhaustParticles;
    [SerializeField] private ParticleSystem idleParticles;

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

        if (moveInput != 0f)
        {
            if (!exhaustParticles.isPlaying)
            {
                idleParticles.Stop();
                exhaustParticles.Play();
                
            }
        }
        else
        {
            if (exhaustParticles.isPlaying)
            {
                exhaustParticles.Stop();
                idleParticles.Play();
            }
        }
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
        turnInput = 0f;
        if (moveInput != 0f)
        {
            turnInput = movement.x;
        }
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
