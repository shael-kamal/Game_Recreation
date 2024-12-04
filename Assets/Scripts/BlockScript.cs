using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BoxCollider2D trigger;
    [SerializeField] private BoxCollider2D body;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (trigger != null)
        {
            if (collision.CompareTag("Player"))
            {
                body.enabled = false;
            }
                
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (trigger != null)
        {
            if(collision.CompareTag("Player"))
            {
                body.enabled = true;
            }
            
        }
    }
}
