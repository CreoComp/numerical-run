using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class UIButton : EventTrigger
{
    public float scale = 1f;

    public bool isPressing;
    public UnityEvent pressedDownEvent;
    public UnityEvent pressedUpEvent;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        isPressing = true;
        pressedDownEvent.Invoke();
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        isPressing = false;
        pressedUpEvent.Invoke();

        transform.localScale = new Vector3(1, 1, 1);
    }
}
