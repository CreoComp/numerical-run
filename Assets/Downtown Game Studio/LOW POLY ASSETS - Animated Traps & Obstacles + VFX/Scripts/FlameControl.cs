using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

/// <summary>
/// Make sure this script is placed on the "FlameSpawner" and that "Flame1", "Flame2" (...) are set as children of "FlameSpawner".
/// </summary>

public class FlameControl : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    public VisualEffect[] flameSpawners;
    private bool isPlaying = false;

    void Start()
    {
        flameSpawners = gameObject.GetComponentsInChildren<VisualEffect>();
        _boxCollider.enabled = false;
        InvokeRepeating("TurnOnOffFire", 1, 2); //Controls the spawning of fire. 
    }

    private void TurnOnOffFire()
    {
        isPlaying = !isPlaying;

        foreach (VisualEffect effect in flameSpawners)
        {
            if (isPlaying)
            {
                _boxCollider.enabled = true;
                effect.Play();
            }
            else
            {
                _boxCollider.enabled = false;
                effect.Stop();
            }
        }
    }
}