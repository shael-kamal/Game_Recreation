using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollingScript : MonoBehaviour
{
    private Transform player;
    private Vector3 initialpos;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        initialpos = transform.position;
        
    }

    private void LateUpdate()
    {
        Vector3 cameraposition = transform.position;
        cameraposition.x = player.position.x;
        cameraposition.x = Mathf.Max(cameraposition.x, initialpos.x);
        transform.position = cameraposition;
    }

}
