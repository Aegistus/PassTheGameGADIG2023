using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerCannon cannon;

    Rigidbody2D rigidbody;

    // Timer for repeatedly firing by holding down the mouse.
    float repeatingFireTimer = 0.0f;

    // Seconds per repeating shot.
    // The value of 0.14f is based, sort of, on measured clicking speed.
    const float REPEATING_FIRE_INTERVAL = 0.14f;

    // Start is called before the first frame update
    void Start()
    {
        cannon = GetComponentInChildren<PlayerCannon>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void FireCannon()
    {
        // Tell the cannon to fire.
        Vector2 dir = cannon.Fire();

        // Apply the impulse from the cannon shot.
        rigidbody.velocity += dir * -4.0f;

        // Reset the repeating fire timer by un-accumulating time.
        while (repeatingFireTimer <= 0)
        {
            repeatingFireTimer += REPEATING_FIRE_INTERVAL;
        }
    }

    void Respawn()
    {
        SceneTransitionCanvas.ReloadSceneStatic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            repeatingFireTimer = 0.0f; // Completely reset timer
            FireCannon();
        }
        else if(Input.GetKey(KeyCode.Mouse0))
        {
            if(repeatingFireTimer <= 0.0)
            {
                FireCannon();
            }
            else
            {
                repeatingFireTimer -= Time.deltaTime;
            }
        }

        // Reset key
        if(Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    void FixedUpdate()
    {
        
    }
}
