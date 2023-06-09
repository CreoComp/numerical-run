using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;
    public Vector3 offset = new Vector3(0, 1.5f, -100);

    [Space]
    public bool changeZoom = false;
    private float startSize;
    public const float endSize = 10;
    public const float maxSpeed = 50;
    public const float zoomSpeed = 5;

    private void Start()
    {
        if (target == null)
            return;

        transform.position = target.position + offset;
        startSize = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 newPosition = target.position + offset;

        transform.position = Vector3.Slerp(transform.position, newPosition, speed * Time.deltaTime);
    }

    private void Update()
    {
        var rig = target.GetComponent<Rigidbody2D>();

        if (!changeZoom || rig == null)
            return;

        float targetSize = Mathf.SmoothStep(startSize, endSize, rig.velocity.magnitude / maxSpeed);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
    }
}