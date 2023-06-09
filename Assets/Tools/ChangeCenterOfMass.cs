using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChangeCenterOfMass : MonoBehaviour
{
    public Transform centerOfMass;

    private void Start()
    {
        var rig = GetComponent<Rigidbody2D>();
        rig.centerOfMass = centerOfMass.localPosition;
    }
}
