using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : MonoBehaviour
{
    private Vector3 _position;

    private void OnEnable()
    {
        _position = transform.localPosition;
    }

    private void LateUpdate()
    {
        transform.position = transform.parent.position + _position;
    }
}
