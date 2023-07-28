using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    [SerializeField] Rigidbody2D cannonBallPrefab;
    SpriteRenderer sprite;

    public const float BallSpeed = 10;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 toMouse = mouse - transform.position;

        float angle = Mathf.Rad2Deg * Mathf.Atan2(toMouse.y, toMouse.x);
        transform.eulerAngles = new Vector3(0, 0, angle);

        // return cannon image back to its original position, if it was moved by recoil (numbers are arbitrary)
        sprite.transform.localPosition = Vector3.MoveTowards(sprite.transform.localPosition, new Vector3(0.25f, -0.001f, 0f), Time.deltaTime);
    }

    // Fires the cannon and returns the direction of fire.
    public Vector2 Fire()
    {
        SoundManager.PlayAtPosition("shoot", transform.position);
        sprite.transform.localPosition = new Vector3(0.15f, -0.001f, 0f); // move back image for recoil effect (numbers are arbitrary)
        
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Move the ball slightly out of the cannon.
        Vector3 moveOutOfCannon = dir * 0.5f;

        Rigidbody2D cannonBall = Instantiate(cannonBallPrefab, transform.position + moveOutOfCannon, transform.rotation);
        cannonBall.velocity = dir * BallSpeed;

        return dir;
    }
}
