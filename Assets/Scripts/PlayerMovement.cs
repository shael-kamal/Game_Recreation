using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    private float speed = 6f;
    private float jumpForce = 20f;
    private bool facingRight = true;
    private bool jumping = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private Sprite dead;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");



        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            audioManager.SFXSound(audioManager.jump);
            animator.SetBool("isJumping", true);

        } else if(IsGrounded())
        {
            animator.SetBool("isJumping", false);
        }



        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        animator.SetFloat("xVelocity",Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private void Flip()
    {
        if (facingRight && horizontal < 0f || !facingRight && horizontal > 0f) {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void Hit()
    {
        GameManager.Instance.PlayerDeath();
        StartCoroutine(HandleCollision());
        
    }

    IEnumerator HandleCollision()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();


        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = dead;

        // Disable collisions to avoid multiple triggers
        Collider2D marioCollider = GetComponent<Collider2D>();
        marioCollider.enabled = false;

        // Apply upward force
        rb.velocity = new Vector2(rb.velocity.x, 15f);

        // Wait for a moment before falling back
        yield return new WaitForSeconds(0.5f);

        // Allow Mario to fall naturally due to gravity
        yield return new WaitForSeconds(1f);

        
        Destroy(gameObject);
        
        GameManager.Instance.TriggerGameOver();
    }




}
