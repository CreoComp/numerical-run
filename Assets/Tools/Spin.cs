using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Spin : MonoBehaviour
{
    public float speed = 0.1f;

    [Space]
    public bool changeDirection = true;
    public bool changeScale = true;

    private Rigidbody2D _rig;
    private Vector3 _lastPosition;

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float s = speed;

        if (changeDirection)
            s *= Mathf.Sign(transform.position.x - _lastPosition.x) * -1;

        if(changeScale && s != 0)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(s);
            transform.localScale = scale;
        }

        _lastPosition = transform.position;

        _rig.MoveRotation(_rig.rotation + s);
    }
}
