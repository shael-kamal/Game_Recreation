using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows.Speech;

public class KoopaScript : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] public GameObject player;
    public string playerTag = "Player";
    public float rayDistance = 3f;
    public float radius = .5f;
    public Sprite shelled;
    public bool isShelled = false;
    public bool shellMoving = false;
    public float moveSpeed = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isShelled)
        {
            if (collision.collider.CompareTag("Player"))
            {
                Vector2 collisionNormal = collision.contacts[0].normal;

                if (collisionNormal.y < -0.5)
                {
                        animator.enabled = false;
                        gameObject.GetComponent<EnemyPatrol>().enabled = false;
                        gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.1f);
                        gameObject.GetComponent<SpriteRenderer>().sprite = shelled;
                        isShelled = true;
                    } else
                {
                    //KILL MARIO
                }
                

            }
        }
        if(shellMoving)
        {
            float dir = collision.transform.position.x - transform.position.x;
            if (dir < 0)
            {
                moveSpeed = 12;
                
            }
            else
                moveSpeed = -12;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isShelled && collision.CompareTag("Player")) {

            float dir = collision.transform.position.x - transform.position.x;
            if (dir < 0)
            {
                moveSpeed = 12;
                shellMoving = true;
            }
            else 
            moveSpeed = -12;
            shellMoving = true;

        }
    }
}
