using Unity.Entities;
using UnityEngine;

public class RingSystem_Entity : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager manager, GameObjectConversionSystem conversionSystem)
    {
        RingObject_Appearance appearance = new RingObject_Appearance();
        manager.AddComponentData(entity, appearance);

        
    }
}