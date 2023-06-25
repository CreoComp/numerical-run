using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Pool/PoolData", fileName = "New PoolData", order = 51)]
public class PoolData : ScriptableObject
{
    [FormerlySerializedAs("PooledObjectType")]
    public GameObjectsTypeId _gameObjectsTypeId;

    public AssetReferenceGameObject PooledObject;
    public int Capacity;
}