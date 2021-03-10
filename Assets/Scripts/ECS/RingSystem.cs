using Unity.Entities;
using Unity.Burst;

public class RingSystem : SystemBase
{
    EntityQuery rotationSpeedQuery;
    EntityQuery appearanceQuery;
    EntityQuery positionQuery;

    protected override void OnCreate()
    {
        rotationSpeedQuery = GetEntityQuery(typeof(RingObject_RotationSpeed), ComponentType.ReadOnly<RingSystem_Entity>());
        appearanceQuery = GetEntityQuery(typeof(RingObject_Appearance), ComponentType.ReadOnly<RingSystem_Entity>());
        positionQuery = GetEntityQuery(typeof(RingObject_Position), ComponentType.ReadOnly<RingSystem_Entity>());
    }

    protected override void OnUpdate()
    {
        
    }

    [BurstCompile]
    struct RotationSpeedJob : IJobEntityBatch
    {
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            
        }
    }

    [BurstCompile]
    struct AppearanceJob : IJobEntityBatch
    {
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            
        }
    }

    [BurstCompile]
    struct PositionJob : IJobEntityBatch
    {
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            
        }
    }
}