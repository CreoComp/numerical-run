using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool
{
    public int Count => _repository.Count;
    public int Capacity { get; }
    public GameObject PooledObject { get; }

    private readonly GameObjectsTypeId _type;
    private readonly Queue<GameObject> _repository = new();

    public Pool(GameObjectsTypeId type, GameObject pooledObject, int capacity)
    {
        PooledObject = pooledObject;
        Capacity = capacity;
        _type = type;
    }

    public GameObject GetPooledObject()
    {
        return _repository.Dequeue();
    }

    public GameObject GetFirst()
    {
        return _repository.Peek();
    }

    public void SetPooledObject(GameObject pooledObject)
    {
        _repository.Enqueue(pooledObject);
    }

    public void ClearPool()
    {
        foreach (GameObject go in _repository)
        {
            Object.Destroy(go);
        }

        _repository.Clear();
    }
}