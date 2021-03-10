using Unity.Entities;
using Unity.Burst;

public class RingSystem : SystemBase
{
    EntityQuery rotationSpeedQuery;
    EntityQuery appearanceQuery;
    EntityQuery positionQuery;

    protected override void OnCreate()
    {

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