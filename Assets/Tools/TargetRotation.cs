using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TargetRotation : MonoBehaviour
{
    public float smooth = 0.5f;

    [HideInInspector]
    public float targetRoation;

    private Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        targetRoation = rig.rotation;
    }

    private void FixedUpdate()
    {
        var angle = transform.eulerAngles;
        angle.z = (Mathf.LerpAngle(angle.z, targetRoation, smooth));

        transform.eulerAngles = angle;
    }
}
