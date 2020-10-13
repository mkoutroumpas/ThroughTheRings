using Assets.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    //  MonoBehaviour object that simulates a spacecraft.
    public class SpacecraftMock : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
        {
            entityManager.AddComponent(entity, typeof(Spacecraft));
        }
    }
}
