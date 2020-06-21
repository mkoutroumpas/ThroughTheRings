using Assets.Scripts.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    public class SpacecraftTranslationSystem : SystemBase //JobComponentSystem
    {
        protected override void OnUpdate()
        {
            var time = Time.DeltaTime;

            Entities.ForEach((ref Translation translation, in Displacement displacement) =>
            {
                translation.Value += new float3(0f, 0f, displacement.Value * time);

            }).ScheduleParallel();
        }

        //protected override JobHandle OnUpdate(JobHandle inputDeps)
        //{
        //    var time = Time.DeltaTime;

        //    var jobHandle = Entities.ForEach((ref Translation translation, in Displacement displacement) =>
        //    {
        //        translation.Value += new float3(0f, 0f, displacement.Value * time);

        //    }).Schedule(inputDeps);

        //    return jobHandle;
        //}


        //[BurstCompile]
        //[RequireComponentTag(typeof(SpaceObject))]
        //struct TranslateJob : IJobForEach<Translation, Displacement>
        //{
        //    [ReadOnly]
        //    public float DeltaTime;

        //    public void Execute(ref Translation translation, ref Displacement displacement)
        //    {
        //        translation.Value += new float3(0f, 0f, displacement.Value * DeltaTime);
        //    }
        //}


        //protected override JobHandle OnUpdate(JobHandle inputDeps)
        //{
        //    var job = new TranslateJob
        //    {
        //        DeltaTime = Time.DeltaTime
        //    };

        //    return job.Schedule(this, inputDeps);
        //}
    }
}
