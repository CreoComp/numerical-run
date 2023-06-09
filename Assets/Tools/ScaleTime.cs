using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTime : MonoBehaviour
{
    public void Scale(float scale)
    {
        Time.timeScale = scale;
    }
}
