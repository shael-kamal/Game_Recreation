using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    [SerializeField] private float sphereCastRadius = 0.5f; // Radius of the sphere
    [SerializeField] private float sphereCastDistance = 1.0f; // Distance to cast the sphere
    [SerializeField] private AudioManager audioManager;
    public GameObject coin;
    public GameObject mushroom;

    public bool hasCoin = true;
    public bool isHit = false;


    public Sprite sprite;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Determine collision point relative to the block
            Vector2 collisionNormal = collision.contacts[0].normal;

            // Check if the collision normal is pointing down (player hitting the block's bottom)
            if (collisionNormal.y > 0.9f)
            {
                OnPlayerDetected();
            }
        }

    }

    void OnPlayerDetected()
    {
        Vector3 offset = new Vector3(0f, 0.5f, 0f);
        if (hasCoin && !isHit)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            audioManager.SFXSound(audioManager.coinCollected);
            isHit = true;
            GameManager.Instance.CollectCoin();
        } else if (!hasCoin && !isHit)
        {
            Instantiate(mushroom, transform.position + offset, Quaternion.identity);
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            isHit = true;
        }
    }
}
