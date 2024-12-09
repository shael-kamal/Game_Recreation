using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCollected : MonoBehaviour
{

    public string playerTag = "Player";
    //public Collider2D coinCollider;


    public AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.CompareTag(playerTag);
        Debug.Log(collision.gameObject + "has collided");
        audioManager.SFXSound(audioManager.coinCollected);
        Destroy(gameObject);
        GameManager.Instance.CollectCoin();
    }

}
