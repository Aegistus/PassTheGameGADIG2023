using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    Vector3 sticky = Vector3.zero;

    // How long the cannon ball will last until it fades & despawns.
    float lifetime = 8.0f;

    // How long the animatino of the cannon ball fading away takes.
    const float FADE_TIME = 1.0f;

    // How transparent the cannon ball is at most.
    const float MAX_OPACITY = 0.9f;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        setOpacity(MAX_OPACITY);
    }

    void setOpacity(float a)
    {
        Color c = spriteRenderer.color;
        c.a = a;
        spriteRenderer.color = c;
    }

    private void FixedUpdate()
    {
        if(sticky.sqrMagnitude > 0)
        {
            transform.position += sticky * 0.5f;
            sticky *= 0.5f;
        }

        if(lifetime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetime -= Time.deltaTime;

            if(lifetime < FADE_TIME)
            {
                float t = (lifetime / FADE_TIME);

                setOpacity(Mathf.Lerp(0.0f, MAX_OPACITY, t));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        // Re-parent ourselves to whatever we collided with.
        Transform newParent = collision.gameObject.transform;
        transform.SetParent(newParent, true);

        // Move slightly further along the collision vector, so we "stick."
        sticky = (Vector3)(collision.relativeVelocity.normalized * -0.2f);

        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.enabled = false;

        // Disable collisions and physics.
        Destroy(col);
        Destroy(body);
    }
}
