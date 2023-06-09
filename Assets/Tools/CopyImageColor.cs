using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class CopyImageColor : MonoBehaviour
{
    public Image image;

    private Image _myImage;

    private void Start()
    {
        _myImage = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        _myImage.color = image.color;
    }
}
