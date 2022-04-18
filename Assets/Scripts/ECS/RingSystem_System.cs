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
                float rA = rotationSpeed.Self.x;
                float3 rV = math.right();

                if (rotationSpeed.Self.y != 0) { rA = rotationSpeed.Self.y; rV = math.up(); }
                if (rotationSpeed.Self.z != 0) { rA = rotationSpeed.Self.z; rV = math.forward(); }

                rotation.Value = math.mul(
                    math.normalize(rotation.Value),
                    quaternion.AxisAngle(rV, rA * deltaTime));

            }).ScheduleParallel();
    }
}