using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float time;
    public bool playOnAwake;

    public UnityEvent endEvent;

    private void Start()
    {
        if(playOnAwake)
            Play();
    }

    public void Play()
    {
        StartCoroutine(PlayCourutine(time));
    }

    public void Play(float time)
    {
        StartCoroutine(PlayCourutine(time));
    }

    public IEnumerator PlayCourutine(float time)
    {
        yield return new WaitForSeconds(time);
        endEvent.Invoke();
    }
}
