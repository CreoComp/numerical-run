using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    public GameObject[] objs;
    private bool _active = true;

    private void Start()
    {
        _active = objs[0].activeInHierarchy;
    }

    public void TurnOn()
    {
        _active = true;
        Activate();
    }

    public void TurnOff()
    {
        _active = false;
        Activate();
    }

    public void Switch()
    {
        _active = !_active;
        Activate();
    }

    public void Activate()
    {
        foreach (var i in objs)
            i.SetActive(_active);
    }
}
