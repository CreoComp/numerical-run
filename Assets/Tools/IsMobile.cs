using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsMobile : MonoBehaviour
{
    public UnityEvent trueEvent;
    public UnityEvent falseEvent;

    private void Awake()
    {
        if(Application.isMobilePlatform)
            trueEvent.Invoke();
        else
            falseEvent.Invoke();
    }
}
