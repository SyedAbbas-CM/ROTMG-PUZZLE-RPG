using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = speed*movementInput;
    }
    private void OnMove(InputValue inputvalue)
    {
        movementInput = inputvalue.Get<Vector2>();
    }
}
