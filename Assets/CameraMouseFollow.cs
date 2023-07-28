using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseFollow : MonoBehaviour
{
    [SerializeField] float followSpeed;
    [SerializeField] float followIntensity;
    Camera cam;


    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        var mouse = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var target = mouse * (followIntensity / 2f);
        var lerpedPosition = Vector3.Lerp(transform.localPosition, target, followSpeed * Time.deltaTime);

        transform.localPosition = new Vector3(lerpedPosition.x, lerpedPosition.y, -23);
    }
}
