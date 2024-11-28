using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject pointA;
    [SerializeField] public GameObject pointB;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform currentpos;
    [SerializeField] public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentpos = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentpos.position - transform.position;
        if (currentpos == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else 
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position , currentpos.position) < 0.5f && currentpos == pointB.transform)
        {
            currentpos = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentpos.position) < 0.5f && currentpos == pointA.transform)
        {
            currentpos = pointB.transform;
        }
    }
}
