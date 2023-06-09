using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class UIButtonScale : MonoBehaviour
{
    public float scale = 1.2f;
    private void Start()
    {
        GetComponent<UIButton>().scale = scale;
    }
}
