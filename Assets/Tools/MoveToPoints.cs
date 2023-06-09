using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveToPoints : MonoBehaviour
{
    public float speed;
    public enum Moving {Looping, Revert}
    public Moving moving = Moving.Revert;

    [Space]
    public Transform[] points;

    private Vector3[] _pointsPos;
    private int _pointNow;
    private int _add = 1;

    private Rigidbody2D _rig;

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();

        _pointsPos = new Vector3[points.Length];
        for(int i = 0; i < points.Length; i++)
            _pointsPos[i] = points[i].position;
    }

    private void FixedUpdate()
    {
        var p = _pointsPos[_pointNow];

        var movePos = Vector3.MoveTowards(transform.position, p, speed);
        _rig.MovePosition(movePos);

        if(Vector3.Distance(transform.position, p) <= 0.1f)
        {
            _pointNow += _add;
        }

        if(moving == Moving.Looping)
            Looping();
        if (moving == Moving.Revert)
            Revert();
    }

    private void Looping()
    {
        if (_pointNow >= _pointsPos.Length)
            _pointNow = 0;
    }

    private void Revert()
    {
        if (_pointNow >= _pointsPos.Length-1)
            _add = -1;
        if (_pointNow <= 0)
            _add = 1;
    }
}
