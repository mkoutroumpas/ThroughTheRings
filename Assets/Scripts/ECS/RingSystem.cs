using Unity.Entities;
using Unity.Burst;

public class RingSystem : SystemBase
{
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
}