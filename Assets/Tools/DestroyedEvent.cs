using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyedEvent : MonoBehaviour
{
    public Component component;
    public UnityEvent destroyEvent;

    private void FixedUpdate()
    {
        if (component == null)
        {
            destroyEvent.Invoke();
            Destroy(this);
        }
    }
}
