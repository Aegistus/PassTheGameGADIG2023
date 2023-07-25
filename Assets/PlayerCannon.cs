using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    public Rigidbody2D cannonBallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 toMouse = mouse - transform.position;

        float angle = Mathf.Rad2Deg * Mathf.Atan2(toMouse.y, toMouse.x);
        transform.eulerAngles = new Vector3(0, 0, angle);

       // Debug.Log("angle = " + transform.eulerAngles);
    }

    // Fires the cannon and returns the direction of fire.
    public Vector2 Fire()
    {
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Move the ball slightly out of the cannon.
        Vector3 moveOutOfCannon = dir * 0.5f;

        Rigidbody2D cannonBall = Instantiate(cannonBallPrefab, transform.position + moveOutOfCannon, transform.rotation);
        cannonBall.velocity = dir * 10.0f;

        return dir;
    }
}
