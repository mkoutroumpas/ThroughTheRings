using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;

[BurstCompile]
public class RingSystem_System : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities
            .WithName("RingSystem_System")
            .ForEach((ref Rotation rotation, in RingObject_RotationSpeed rotationSpeed) =>
            {
                rotation.Value = math.mul(
                    math.normalize(rotation.Value),
                    quaternion.AxisAngle(math.up(), rotationSpeed.Self.y * deltaTime));
            }).ScheduleParallel();
    }
}