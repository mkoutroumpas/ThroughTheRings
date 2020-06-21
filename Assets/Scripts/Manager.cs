using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject[] GameObjectsToMove;
    
    private const int UnitLengthInMeters = 1000;

    private EntityManager _entityManager;

    private void Start()
    {
        //var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        foreach (var go in GameObjectsToMove)
        {
            var position = go.transform.position;

            //var entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(go, settings);

            var entityInstance = _entityManager.Instantiate(go);

            _entityManager.AddComponentData(entityInstance, new Translation { Value = position });

            _entityManager.AddComponentData(entityInstance, 
                new Displacement { Value = (Forward ? 1 : -1) * TranslationRate * VelocityMetersPerSecond / UnitLengthInMeters });
        }
    }
}
