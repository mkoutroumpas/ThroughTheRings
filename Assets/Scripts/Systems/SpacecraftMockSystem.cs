using Assets.Scripts.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [BurstCompile]
    public class SpacecraftMockSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var time = Time.DeltaTime;

            Entities.ForEach((ref Translation translation, in Velocity velocity) =>
            {
                translation.Value += new float3(0f, 0f, velocity.Value * time);

            }).Schedule();
        }
    }
}
