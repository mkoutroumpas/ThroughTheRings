using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using System.Collections.Generic;

public class Test_Spawner : MonoBehaviour
{
    #region Public properties
    public GameObject RingObjectPrefab;
    #endregion

    #region Private variables
    EntityManager EntityManager;
    Entity Entity;
    #endregion

    void Start()
    {
        this.EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        this.Entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(RingObjectPrefab, settings);

        var instance = this.EntityManager.Instantiate(this.Entity);
        var position = transform.TransformPoint(new Vector3(10, 0, 10));

        this.EntityManager.SetComponentData(instance, new Translation { Value = position });
    }
    
}