using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScript : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] public GameObject player;
    public string playerTag = "Player";
    public Sprite squished;


    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       



        if (collision.gameObject.CompareTag("Player"))
        {
            // Determine the collision normal relative to the enemy
            Vector2 collisionNormal = collision.contacts[0].normal;

            if (collisionNormal.y < -0.8f) // Player hits the enemy from above
            {
                animator.enabled = false;
                gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.1f);
                gameObject.GetComponent<SpriteRenderer>().sprite = squished;
                Destroy(gameObject, 0.5f);
                Debug.Log("done");
                PlayerBounce(collision.gameObject);
            }
           
        }
    }

    private void PlayerBounce(GameObject player)
    {
        // Add an upward force to simulate a bounce
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 10f); // Adjust bounce height as needed
        }

        Debug.Log("Player bounced!");
    }

}
