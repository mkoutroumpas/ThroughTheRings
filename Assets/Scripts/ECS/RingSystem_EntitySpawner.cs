using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using System.Collections.Generic;

public class RingSystem_EntitySpawner : MonoBehaviour
{
    #region Public properties
    public GameObject RingObjectPrefab;
    #endregion

    List<(float Angle, float YOverhead, Color Color)> _RingLayers = new List<(float, float, Color)>
    {
        (0.0f, -4200f, Color.green),
        (0.25f, -3400f, Color.white),
        (0.5f, -2600f, Color.blue),
        (0.75f, -1800f, Color.grey),
        (1.0f, -1000f, Color.yellow),
        (1.25f, -200f, Color.magenta),
        (1.5f, 200f, Color.cyan),
        (1.75f, 1000f, Color.white),
        (2.0f, 1800f, Color.blue),
        (2.25f, 2600f, Color.grey),
        (2.5f, 3400f, Color.yellow),
        (2.75f, 4200f, Color.red)
    };

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