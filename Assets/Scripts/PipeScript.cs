using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int pipeLength;
    [SerializeField] private GameObject pipeTop;
    [SerializeField] private GameObject pipeBottom;
    void Start()
    {
        GeneratePipe(pipeTop,pipeBottom);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneratePipe(GameObject head, GameObject body)
    {
        // Instantiate the head of the pipe
        GameObject pipeHead = Instantiate(head, transform.position, Quaternion.identity);
        pipeHead.transform.SetParent(transform);

        Vector3 nextPosition = pipeHead.transform.position;

        // Instantiate the body segments
        for (int i = 1; i < pipeLength; i++)
        {
            nextPosition.y -= 0.5f; 
            GameObject pipeBody = Instantiate(body, nextPosition, Quaternion.identity);
            pipeBody.transform.SetParent(pipeHead.transform); 
        }
    }
}
