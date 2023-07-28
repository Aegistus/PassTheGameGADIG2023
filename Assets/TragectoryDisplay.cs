using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TragectoryDisplay : MonoBehaviour
{
    [SerializeField] int pointCount;
    [SerializeField] float stepSize;
    LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        var points = new List<Vector3>(pointCount);

        var trajectory = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        trajectory.z = 0;
        trajectory = trajectory.normalized; // must be done after setting z to 0
        var currentPos = transform.position + trajectory * .5f; // slight movement copied from PlayerCannon
        var currentVel = trajectory * PlayerCannon.BallSpeed;

        var timeStep = Time.fixedDeltaTime / Physics2D.velocityIterations * stepSize;
        Vector3 gravity = Physics2D.gravity * (timeStep * timeStep);
        Vector3 moveStep = currentVel * timeStep;

        points.Add(currentPos);
        for (int i = 0; i < pointCount; i++)
        {
            moveStep += gravity;
            currentPos += moveStep;
            points.Add(currentPos);
        }

        line.positionCount = pointCount;
        line.SetPositions(points.ToArray());
    }
}
