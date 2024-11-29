using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaMovement : MonoBehaviour
{


    private bool facingLeft = true;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }
    private void Flip()
    {
        if (facingLeft && rb.velocityX > 0|| !facingLeft && rb.velocityX < 0)
        {
            facingLeft = !facingLeft;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}
