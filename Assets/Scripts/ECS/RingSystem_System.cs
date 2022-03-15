using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Jobs;

public class RingSystem_System : JobComponentSystem
{
    [BurstCompile]
    [RequireComponentTag(typeof(RingObject_Entity))]
    struct RotateJob : IJobForEach<Rotation, RingObject_RotationSpeed>
    {
        [ReadOnly]
        public float DeltaTime;

        public void Execute(ref Rotation rotation, ref RingObject_RotationSpeed rotationSpeed)
        {
            
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return default;
    }
}