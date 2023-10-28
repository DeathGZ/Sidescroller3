using UnityEngine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.InputSystem;
using static UnityEngine.LightAnchor;

public class CharacterMovement : MonoBehaviour
{
    private Player_InputActions inputActions;/// <summary>                                             /// input action declaration                                             /// </summary>
    private InputAction moveAction;
    public InputAction jumpAction;
    public float rollDistance = 10;
    public InputAction blockAction;
    public InputAction rollAction;
    public InputAction attackAction;
    public bool facingRight = true;
    public Vector2 moveInput;
    public float jumpStrength = 10;

    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator anim;
    


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


        if (facingRight == false && moveInput.x > 0)
            Flip();
        else if (facingRight == true && moveInput.x < 0)
            Flip();

        
    }

   void FixedUpdate()
    {
        //rb.velocity = new Vector2(moveInput.x * speed, moveInput.y * speed);
        rb.AddForce(moveInput.x * speed * Vector2.right);
    }
  

     void OnDisable()
    {
        inputActions.Disable();
        attackAction.Disable();
        blockAction.Disable();
        rollAction.Disable();
        jumpAction.Disable();
    }

    

    void Flip()
    {
        //Flip player sprite

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
        rb.velocity = new Vector2(0f, rb.velocity.y);
        rb.velocity += new Vector2(transform.localScale.x, 0f) * rollDistance;
    }

    void Jump()
    {
        anim.SetTrigger("ActionTrigger");
        anim.SetInteger("Actionid", 9);
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.velocity += Vector2.up * jumpStrength;
    }
           
     
}