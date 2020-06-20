using Assets.Scripts.Components;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] PrefabsToMove;

    private const int VelocityMetersPerSecond = 10;
    private const bool Forward = true;
    private const int UnitLengthInMeters = 1000;
    private const float TranslationRate = 0.1f;

    private EntityManager _entityManager;

    private void Start()
    {
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        foreach (var prefab in PrefabsToMove)
        {
            var position = prefab.transform.position;

            var entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, settings);

            var entityInstance = _entityManager.Instantiate(entity);
            
            _entityManager.SetComponentData(entityInstance, 
                new Translation { Value = position });
            _entityManager.SetComponentData(entityInstance, 
                new Displacement { Value = (Forward ? 1 : -1) * TranslationRate * VelocityMetersPerSecond / UnitLengthInMeters });
        }
    }
}
