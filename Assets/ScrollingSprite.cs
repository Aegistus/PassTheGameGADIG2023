using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingSprite : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;

    const float loopInterval = 2;
    float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        transform.position += Vector3.right * (speed * Time.deltaTime);
        
        var distanceMoved = transform.position.x - startX;
        if (distanceMoved > loopInterval)
            transform.position = new Vector3(transform.position.x - loopInterval, transform.position.y, transform.position.z);
    }
}
