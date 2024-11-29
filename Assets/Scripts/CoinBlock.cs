using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    public string playerTag = "Player"; // Tag assigned to the player object
    public float circleRadius = 0.5f; // Radius of the circle for detection
    public float detectionOffset = 0.5f; // Distance below the block to start the detection
    public GameObject coin;
    public GameObject mushroom;

    public bool hasCoin = true;
    public bool isHit = false;


    public Sprite sprite;

    private void Awake()
    {
        

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Define the circle's center position below the block
        Vector2 circleCenter = new Vector2(transform.position.x, transform.position.y - detectionOffset);

        // Perform a circle cast to detect colliders within the specified radius
        Collider2D hit = Physics2D.OverlapCircle(circleCenter, circleRadius);

        // Debugging: Draw the circle in the Scene view
        Debug.DrawRay(circleCenter, Vector2.up * 0.1f, Color.green, 1.0f); // Center point
        Debug.DrawRay(circleCenter, Vector2.right * circleRadius, Color.red, 1.0f); // Radius visualization

        // Check if the detected collider has the correct tag
        if (hit != null && hit.CompareTag(playerTag))
        {
            OnPlayerDetected(hit.gameObject);
        }
        
    }

    void OnPlayerDetected(GameObject player)
    {
        Vector3 offset = new Vector3(0f, 0.5f, 0f);
        if (hasCoin && !isHit)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            isHit = true; 
        } else if (!hasCoin && !isHit)
        {
            Instantiate(mushroom, transform.position + offset, Quaternion.identity);
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            isHit = true;
        }
    }
}
