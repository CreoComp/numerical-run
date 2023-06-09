using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Vector3 _rotation;

    private void OnEnable()
    {
        _rotation = transform.eulerAngles;
    }

    private void LateUpdate()
    {
        transform.eulerAngles = _rotation;
    }
}
