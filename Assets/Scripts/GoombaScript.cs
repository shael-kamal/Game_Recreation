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
        if(collision.collider.CompareTag("Player")){
            if(collision.collider.transform.position.y > transform.position.y){
                animator.enabled = false;
                gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.1f);
                gameObject.GetComponent<SpriteRenderer>().sprite = squished;
                Destroy(gameObject, 0.5f);
                Debug.Log("done");
            }

            
            
        }
    }

}
