using Unity.Entities;
using UnityEngine;

public class RingSystem_EntityConverter : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager manager, GameObjectConversionSystem conversionSystem)
    {
        RingObject_Appearance appearance = new RingObject_Appearance { Color = Color.gray, Scale = new Vector3(1f, 1f, 1f) };
        manager.AddComponentData(entity, appearance);

        RingObject_Position position = new RingObject_Position { Angle = 0f, Radius = 10000f };
        manager.AddComponentData(entity, position);

        RingObject_RotationSpeed rorationSpeed = new RingObject_RotationSpeed { SelfRotationSpeed = new Vector3(1f, 1f, 1f), SystemRotationSpeed = new Vector3(1f, 1f, 1f) };
        manager.AddComponentData(entity, rorationSpeed);

        Transform gOT = gameObject.transform;

        RingObject_SystemData systemData = new RingObject_SystemData 
        { 
            PlanetRadius = gOT.localScale.z / 2
        };
        manager.AddComponentData(entity, systemData);
    }
}