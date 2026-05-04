using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualHandMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 moveInput;

    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb2D.linearVelocity = moveInput * moveSpeed;
    }

    public void SetMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector3>();
    }
}
