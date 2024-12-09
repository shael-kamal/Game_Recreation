using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontal;
    private float speed = 6f;
    private float jumpForce = 13f;
    private bool facingRight = true;
    private bool jumping = false;
    private float currentJumpForce = 0f;
    private float jumpTimer = 0f;
    private bool isBig = false;
    private float cooldowntime = 0f;
    private float cooldownMax = 1.5f;


    public float maxJumpForce = 15f; // Maximum jump force
    public float chargeRate = .1f;  // Rate of charging jump force
    public float maxJumpTime = 0.1f; // Maximum time for jump force to be applied


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private Sprite dead;

    public AudioManager audioManager;
    private bool cooling = false;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooling)
        {
            if (cooldowntime <= cooldownMax)
            {
                cooldowntime += Time.deltaTime;
            }

            if (cooldowntime >= cooldownMax)
            {
                cooling = false;
            }
        }
        


        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {

            jumping = true;
            currentJumpForce = 10f;
            jumpTimer = 0f;


            //rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            audioManager.SFXSound(audioManager.jump);
            animator.SetBool("isJumping", true);

        }
        if (Input.GetButton("Jump") && jumping)
        {
            jumpTimer += Time.deltaTime;

            if (jumpTimer <= maxJumpTime)
            {
                currentJumpForce += chargeRate * Time.deltaTime;
                currentJumpForce = Mathf.Clamp(currentJumpForce, 0f, maxJumpForce);

                rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            jumping = false;
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
        if (animator.GetBool("isBig"))
        {
            GameManager.Instance.PowerDown();
            Vector3 tempPos = transform.position;
            animator.SetBool("isBig", false);
            gameObject.transform.position = tempPos - new Vector3(0f, 0.25f, 0f);
            gameObject.GetComponent<Collider2D>().offset = new Vector2(-0.01f, 0f); //change
            gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.33f, 0.46f);//change 
            groundCheck.transform.position = groundCheck.transform.position - new Vector3(0.01f, -0.27f, 0f);
            cooling = true;
            //gameObject.GetComponentInChildren<Transform>().position = new Vector3(0.02f,0.4f,0f);
            
            
        }
        else
        {
            if (!cooling)
            {
                GameManager.Instance.PlayerDeath();
                StartCoroutine(HandleCollision());
            }
        }
        
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


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Mushroom"))
        {
            if (!animator.GetBool("isBig"))
            {
                GameManager.Instance.PowerUp();
                Vector3 tempPos = transform.position;
                animator.SetBool("isBig", true);
                gameObject.transform.position = tempPos + new Vector3(0f, 0.25f, 0f);
                gameObject.GetComponent<Collider2D>().offset = new Vector2(0f, -0.02f);
                gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.47f, 0.95f);
                groundCheck.transform.position = groundCheck.transform.position + new Vector3(0.01f, -0.27f, 0f);
                //gameObject.GetComponentInChildren<Transform>().position = new Vector3(0.02f,0.4f,0f);
                Destroy(collision.gameObject,0.1f);
                
            } 
        }
    }

}
