using Unity.Entities;
using UnityEngine;

public class RingSystem_Entity : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager manager, GameObjectConversionSystem conversionSystem)
    {
        RingObject_Appearance appearance = new RingObject_Appearance { Color = Color.gray, PrimitiveType = PrimitiveType.Cube, Scale = new Vector3(1f, 1f, 1f) };
        manager.AddComponentData(entity, appearance);

        RingObject_Position position = new RingObject_Position();
        manager.AddComponentData(entity, position);

        RingObject_RotationSpeed rorationSpeed = new RingObject_RotationSpeed();
        manager.AddComponentData(entity, rorationSpeed);
    }
}