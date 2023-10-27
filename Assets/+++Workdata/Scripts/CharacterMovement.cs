using UnityEngine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.InputSystem;

public class CaracterMovement : MonoBehaviour
{
    #region Variables
    private Player_InputActions inputActions;/// <summary>                                             /// input action declaration                                             /// </summary>
    private InputAction moveAction;
    public InputAction jumpAction;
    public InputAction blockAction;
    public InputAction rollAction;
    public InputAction attackAction;
    bool facingRight = true;
    public Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    public float jumpStrength;
    #endregion


    private void Awake()
    {
        inputActions = new Player_InputActions();
        moveAction = inputActions.Player.Move;
        attackAction = inputActions.Player.Attack;
        blockAction = inputActions.Player.Block;
        rollAction = inputActions.Player.Roll;
        jumpAction = inputActions.Player.Jump;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        attackAction.Enable();
        blockAction.Enable();
        rollAction.Enable();
        jumpAction.Enable();
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
        if (facingRight == false && moveInput.x > 0)
           Flip();
        else if (facingRight == true && moveInput.x < 0)
           Flip();

        if (attackAction.triggered)
            Attack();

        if (blockAction.triggered)
            Block();

        if (rollAction.triggered)
            Roll();

        if (jumpAction.triggered)
            Jump();
      
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * speed, moveInput.y * speed);         // move input wird auf der jeweiligen achsen,                                                                              // mal speed (oben deklariert)
    }
  

    private void OnDisable()
    {
        inputActions.Disable();
        attackAction.Disable();
        blockAction.Disable();
        rollAction.Disable();
        jumpAction.Disable();
    }

    void Flip()
    #region Voids
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Attack()
    {
        anim.SetTrigger("ActionTrigger");
        anim.SetInteger("Actionid", 0);
    }

    void Block()
    {
        anim.SetTrigger("ActionTrigger");
        anim.SetInteger("Actionid", 10);
    }

    void Roll()
    {
        anim.SetTrigger("ActionTrigger");
        anim.SetInteger("Actionid", 11);
    }

    void Jump()
    {
        anim.SetTrigger("ActionTrigger");
        anim.SetInteger("Actionid", 9);
    }
    #endregion
}
