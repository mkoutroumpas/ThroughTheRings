using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[AddComponentMenu("RingSystem")]
[ConverterVersion("mkoutroumpas", 1)]
public class RingSystem_EntityConverter : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject Prefab;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(Prefab);
    }
    public void Convert(Entity entity, EntityManager manager, GameObjectConversionSystem conversionSystem)
    {
        RingObject_Appearance appearance = new RingObject_Appearance { Color = Color.gray, PrimitiveType = PrimitiveType.Cube, Scale = new Vector3(1f, 1f, 1f) };
        manager.AddComponentData(entity, appearance);

        RingObject_Position position = new RingObject_Position { Angle = 0f, Radius = 10000f };
        manager.AddComponentData(entity, position);

        RingObject_RotationSpeed rorationSpeed = new RingObject_RotationSpeed { SelfRotationSpeed = new Vector3(0f, 0f, 0f), SystemRotationSpeed = new Vector3(1f, 1f, 1f) };
        manager.AddComponentData(entity, rorationSpeed);
    }
}