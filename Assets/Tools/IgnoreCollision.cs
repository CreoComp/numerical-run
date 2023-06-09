using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public GameObject[] ignore;
    private List<Collider2D> colliders = new();

    private void Start()
    {
        colliders.AddRange(GetComponentsInChildren<Collider2D>());
        foreach(var i in ignore)
        {
            colliders.AddRange(i.GetComponentsInChildren<Collider2D>());
        }

        for (int i = 0; i < colliders.Count; i++)
        {
            for(int k = i + 1; k < colliders.Count; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }
    }

    public void StopIgnore(Collider2D col)
    {
        foreach(var i in colliders)
        {
            Physics2D.IgnoreCollision(i, col, false);
        }
    }
}
