using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;

public class RingSystem_EntitySpawner : MonoBehaviour
{
    public GameObject RingObjectPrefab;
    void Start()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(RingObjectPrefab, settings);

        var instance = entityManager.Instantiate(prefab);
        var position = transform.TransformPoint(new Vector3(0, 0, 0));
        entityManager.SetComponentData(instance, new Translation { Value = position });
    }
}