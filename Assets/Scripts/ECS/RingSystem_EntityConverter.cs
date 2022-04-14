using Unity.Entities;
using UnityEngine;

public class RingSystem_EntityConverter : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager manager, GameObjectConversionSystem conversionSystem)
    {
        manager.AddComponent(entity, typeof(RingObject_RotationSpeed));
    }
}