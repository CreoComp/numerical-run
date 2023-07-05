using UnityEngine;

public class PickableObjectSpawner : MonoBehaviour
{
    private GameObjectsTypeId _pickableObjectTypeId;
    private IPoolService _poolService;
    private Transform _parent;
    private Vector3 _position;
    private float _chanceOfStaying = 0.5f;
    private Quaternion _rotation;

    public void Construct(GameObjectsTypeId pickableObjectTypeId,
        IPoolService poolService, Transform parent, Vector3 position, Quaternion rotation)
    {
        _pickableObjectTypeId = pickableObjectTypeId;
        _poolService = poolService;
        _parent = parent;
        _position = position;
        _rotation = rotation;
    }

    public GameObject Spawn()
    {
        GameObject pickableObject = default;

        if (GetChance())
        {
            pickableObject = _poolService.Get(_pickableObjectTypeId);
            pickableObject.transform.SetParent(_parent, true);
            pickableObject.transform.position = _position;
            pickableObject.transform.rotation = _rotation;
        }

        return pickableObject;
    }

    private bool GetChance()
    {
        return true; //Random.value < _chanceOfStaying;
    }
}