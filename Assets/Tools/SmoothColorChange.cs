using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ColorEvent : UnityEvent<Color> { }

[ExecuteAlways]
public class SmoothColorChange : MonoBehaviour
{
    public Color sColor = Color.white;

    [Space]
    public ColorEvent colorEvent = new ColorEvent();

    private Color _colorNow;
    private Color _color;

    private float _time = 0;
    private float _sTime = 0f;

    private void Awake()
    {
        _colorNow = _color = sColor;
    }

    private void Update()
    {
        _time -= Time.deltaTime;

        if (Time.timeSinceLevelLoad <= 0.5f)
        {
            _colorNow = _color;
            colorEvent.Invoke(_colorNow);
            return;
        }
        if (_time <= 0)
        {
            return;
        }

        _colorNow = Color.Lerp(_colorNow, _color, (_sTime - _time) / _sTime);
        colorEvent.Invoke(_colorNow);
    }

    public void Change(Color color, float time = 0.5f)
    {
        _color = color;

        _time = _sTime = time;

        if (time <= 0)
        {
            _colorNow = color;
            colorEvent.Invoke(_colorNow);
        }
    }

    public void BackToStartColor(float time = 0.5f)
    {
        _color = sColor;
        _time = _sTime = time;
    }
}
