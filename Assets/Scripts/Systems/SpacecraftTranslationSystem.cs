using Assets.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [BurstCompile]
    public class SpacecraftTranslationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var time = Time.DeltaTime;

            Entities.WithAll<Spacecraft>().ForEach((ref Translation translation, in Displacement displacement) =>
            {
                translation.Value += new float3(0f, 0f, displacement.Value * time);

            }).ScheduleParallel();
        }
    }
}
