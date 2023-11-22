using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CharacterMovement : MonoBehaviour
{
    #region Private Variables
    #region Input Variables
    /// <summary>
    /// Reference to the input actiom map file / Script
    /// </summary>
    private Player_InputActions inputActions;

    private InputAction moveAction;
    private InputAction rollAction;
    private InputAction attackAction;
    private InputAction jumpAction;
    private InputAction rollAttackAction;   
    #endregion
    #endregion

    #region Public Variables
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public LayerMask groundLayer;

    public Vector3 groundcheckBoxSize;
    public Vector3 groundcheckBoxPosition;
    public Vector2 moveInput;

    public float speed = 5f;
    public float jumpForce;
    public float rollForce;
    public float health = 100;

    public bool isGrounded;

    public bool isFreezed;

    public bool isRolling;

    public bool isMoving;
    #endregion

    #region Unity Event Functions
    private void Awake()
    {
        inputActions = new Player_InputActions();
        moveAction = inputActions.Player.Move;
        rollAction = inputActions.Player.Roll;
        attackAction = inputActions.Player.Attack;  
        jumpAction = inputActions.Player.Jump;
        rollAttackAction = inputActions.Player.RollAttack;    
    }


    private void OnEnable()
    {
        inputActions.Enable();

        moveAction.performed += Move;
        moveAction.canceled += Move;

        attackAction.performed += Attack;
        rollAction.performed += Roll;
        jumpAction.performed += Jump;
        rollAttackAction.performed += RollAttack;
    }

   

    private void FixedUpdate()
    {
        if (isFreezed == true) { return; }
        isGrounded = Physics2D.OverlapBox(gameObject.transform.position + groundcheckBoxPosition, groundcheckBoxSize, 0, groundLayer);

        anim.SetBool("isGrounded", isGrounded);

        if (!isRolling)
        {
            rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        }
    }

    private void LateUpdate()
    {
        anim.SetBool("isGrounded", isGrounded);
    }

    private void OnDisable()
    {
        inputActions.Disable();

        moveAction.performed -= Move;
        moveAction.canceled -= Move;

        attackAction.performed -= Attack;
        rollAction.performed -= Roll;
        jumpAction.performed -= Jump;
        rollAttackAction.performed -= RollAttack;
    }
    #endregion

    void Move(CallbackContext ctx)
    {
        
        moveInput = moveAction.ReadValue<Vector2>();

        if (ctx.performed)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        {
            if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            if (moveInput.x != 0)
            {
                anim.SetFloat("MovementSpeed", 5);
            }
            else
            {
                anim.SetFloat("MovementSpeed", 0);
            }

        }
    }

    void Attack(CallbackContext ctx)
    {
        if (isGrounded)
        {
            if (!isRolling)
            {
                AnimationCallAction(10);
            }
            else
            {
                AnimationCallAction(11);
            }
        }
        
      
    }

    void Roll(CallbackContext ctx)
    {
        if (!isRolling && isGrounded)
        {
            isRolling = true;

            if (spriteRenderer.flipX)
            {
                rb.AddForce(new Vector2(-rollForce, 0), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(rollForce, 0), ForceMode2D.Impulse);
            }
            
            Invoke("EndRolling", 0.5f);
            AnimationCallAction(11);
        }
       
    }

    void RollAttack(CallbackContext ctx)
    {
        if (!isRolling)
        {
            isRolling = true;

            if (spriteRenderer.flipX)
            {
                rb.AddForce(new Vector2(-rollForce, 0), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(rollForce, 0), ForceMode2D.Impulse);
            }

            Invoke("EndRolling", 0.5f);
            AnimationCallAction(1);
        }

    }

    public void EndRolling()
    {
        isRolling = false;
    }

    void Jump(CallbackContext ctx)
    {   
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            AnimationCallAction(2);
        }
       
    }

    void AnimationCallAction(int id)
    {
        anim.SetTrigger("ActionTrigger");
        anim.SetInteger("ActionId", id);
    }

    public void GetDmg()
    {
        print("Game Over");

        AnimationCallAction(4);
    }

    public void Freeze()
    {
        AnimationCallAction(5);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gameObject.transform.position + groundcheckBoxPosition, groundcheckBoxSize);
    }

    public void death()
    {
        Destroy(gameObject, 1f);
        anim.SetTrigger("Death");
    }

}
