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
    private SpriteRenderer spriteRenderer;
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator anim;

  
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
        if (moveInput.x != 0)
        {
            anim.SetFloat("MovementSpeed", 5);
        }
        else
        {
            anim.SetFloat("MovementSpeed", 0);
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * speed, moveInput.y * speed);         // move input wird auf der jeweiligen achsen,                                                                              // mal speed (oben deklariert)
    }
  

    private void OnDisable()
    {
        inputActions.Disable();
    }

    
}
