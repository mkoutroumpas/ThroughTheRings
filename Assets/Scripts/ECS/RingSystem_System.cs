using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;

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
            rotation.Value = quaternion.AxisAngle(math.up(), rotationSpeed.Self.x * DeltaTime);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        RotateJob rotateJob = new RotateJob
        {
            DeltaTime = Time.DeltaTime
        };

        return rotateJob.Schedule(this, inputDeps);
    }
}