using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaMovement : EnemyPatrol
{


    private bool facingLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        
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
