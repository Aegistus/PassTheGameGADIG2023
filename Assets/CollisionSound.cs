using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [SerializeField] string soundName;

    void OnCollisionEnter2D(Collision2D col)
    {
        var volume = Mathf.Clamp(col.relativeVelocity.magnitude - 10, -10f, 2f);
        SoundManager.PlayAtPosition(soundName, transform.position, volume);
    }
}
