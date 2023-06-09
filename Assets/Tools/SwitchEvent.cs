using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchEvent : MonoBehaviour
{
    public bool state = false;

    public UnityEvent trueEvent;
    public UnityEvent falseEvent;
    public BoolEvent stateEvent;

    public void Switch()
    {
        state = !state;

        if (state)
            trueEvent.Invoke();
        else
            falseEvent.Invoke();

        stateEvent.Invoke(state);
    }
}
