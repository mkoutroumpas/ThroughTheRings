﻿using Assets.Scripts.Components;
using Unity.Burst;
using Unity.Collections;
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

            Entities.WithAll<SpacecraftMock>().ForEach((ref Translation translation, in Velocity velocity) =>
            {
                translation.Value += new float3(0f, 0f, velocity.Value * time);

            }).Schedule();
        }
    }
}
