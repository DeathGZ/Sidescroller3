using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

    public float Of { get; private set; }




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Of);
        spriteRenderer.flipX = rb.velocity.x < Of;
    }
}

