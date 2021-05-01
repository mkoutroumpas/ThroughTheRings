using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;

public class RingSystem_Creator : JobComponentSystem
{
    #region Constants
    const float RingWidth = 50000.0f;
    const int NumOfRingsAB = 20;
    const int RingAngleStep = 3;
    const float StdDeviation = 0.1f;
    const int SystemAngleSpanDegs = 360;
    const float MinRingObjectScale =  0.001f, MaxRingObjectScale = 250.0f;
    const float MinDeviation = -5000.0f, MaxDeviation = 5000.0f;
    const float MinYDeviation = -500.0f, MaxYDeviation = 500.0f;
    const float MaxSelfRotationSpeed = 10.0f, MinSelfRotationSpeed = 0.0f;
    const float DiffSelfRotSpeed = MaxSelfRotationSpeed - MinSelfRotationSpeed;
    const float MaxSystemRotationSpeed = 0.005f, MinSystemRotationSpeed = 0.0025f;
    const float DiffSystemRotSpeed = MaxSystemRotationSpeed - MinSystemRotationSpeed;
    const bool EnableRingObjectsRotation = true;
    const FieldDepths FieldDepth = FieldDepths.Near;
    const Distributions Distribution = Distributions.White;
    #endregion

    #region Private variables
    EntityQuery ringObjectQuery;
    BeginInitializationEntityCommandBufferSystem entityCommandBufferSystem;
    #endregion

    #region System overrides
    protected override void OnCreate()
    {
        
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return default;
    }
    #endregion

    #region Jobs
    [BurstCompile]
    [RequireComponentTag(typeof(RingObject_Position), typeof(RingObject_RotationSpeed), typeof(RingObject_SystemData), typeof(RingObject_Appearance))]
    struct MainJob : IJobEntityBatch
    {
        public float DeltaTime;
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            
        }
    }

    #endregion

    #region Support
    struct RingLayerData
    {
        public float Angle { get; set; }
        public float YOverhead { get; set; }
        public Color Color { get; set; }
    }
    int GetSizeAndDistanceMultiplier(FieldDepths fieldDepth) => fieldDepth == FieldDepths.Far ? 100 : 1; 

    float GetRingObjectRadialDistance(int ringId, float ringSystemA) => ringSystemA + ringId * RingWidth * GetSizeAndDistanceMultiplier(FieldDepth) / (NumOfRingsAB + 1);

    float GetRingObjectSize(float minSize = 1f, float maxSize = 1000f, Distributions distribution = default) 
    {
        if (distribution == Distributions.White) return (float)(Random.Range(0.0f, 1.0f) * (maxSize - minSize) + minSize);
        if (distribution == Distributions.Normal) // See https://stackoverflow.com/a/218600
        {
            float u1 = 1.0f - Random.Range(0.0f, 1.0f);
            float u2 = 1.0f - Random.Range(0.0f, 1.0f);
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
            return (maxSize - minSize) / 2 + StdDeviation * randStdNormal;
        }
        if (distribution == Distributions.HalfNormal)
        {
            return Mathf.Sqrt(2f) / (StdDeviation * Mathf.Sqrt(Mathf.PI)) 
                * Mathf.Exp(-Mathf.Pow((float)(Random.Range(0.0f, 1.0f) * (maxSize - minSize) + minSize), 2f) / (2 * Mathf.Pow(StdDeviation, 2)));
        }

        return 0.0f;
    }
    #endregion
}