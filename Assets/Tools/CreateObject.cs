using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreateObject : MonoBehaviour
{
    public GameObject obj;
    public Transform transform_;
    public float limit = -1;
    public bool checkIfCreated = true;

    [Space]
    public UnityEvent createEvent;

    private GameObject _obj = null;
    private float count;

    private void Start()
    {
        if (transform_ == null)
            transform_ = transform;
    }

    public void Create()
    {
        if (obj == null || (_obj != null && Vector2.Distance(_obj.transform.position, transform_.position) <= 0.5f && checkIfCreated))
            return;

        count++;

        _obj = Instantiate(obj, null);
        _obj.transform.position = transform_.position;
        createEvent.Invoke();

        if(count == limit)
        {
            Destroy(this.gameObject);
        }
    }
}
