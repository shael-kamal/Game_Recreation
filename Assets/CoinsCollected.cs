using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCollected : MonoBehaviour
{

    public string playerTag = "Player";
    public Collider2D coinCollider;
    public GameObject coinPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.CompareTag(playerTag);
        Debug.Log(collision.gameObject + "has collided");
        Destroy(coinPrefab);
    }



}
