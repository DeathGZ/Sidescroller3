using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CaracterMovement : MonoBehaviour
{
    private Player_InputActions inputActions;/// <summary>
    /// input action declaration
    /// </summary>
    private InputAction moveAction;

    public Vector2 moveInput;

    public float speed = 5f;
    public Rigidbody2D rb;

    private void Awake()
    {
        inputActions = new Player_InputActions();
        moveAction = inputActions.Player.Move;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        
    }


    private void OnDisable()
    {
        inputActions.Disable();
    }

}