using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class RingSystem_Creator : SystemBase
{
    #region Constants
    const float RingWidth = 50000.0f;
    const int NumOfRingsAB = 20;
    const int RingAngleStep = 3;
    const float StdDeviation = 0.1f;
    const float MinRingObjectScale =  0.001f, MaxRingObjectScale = 250.0f, UniformRingObjectScale = 250.0f;
    const float MinDeviation = -5000.0f, MaxDeviation = 5000.0f;
    const float MinYDeviation = -500.0f, MaxYDeviation = 500.0f;
    const float MaxSelfRotationSpeed = 10.0f, MinSelfRotationSpeed = 0.0f;
    const float DiffSelfRotSpeed = MaxSelfRotationSpeed - MinSelfRotationSpeed;
    const float MaxSystemRotationSpeed = 0.005f, MinSystemRotationSpeed = 0.0025f;
    const float DiffSystemRotSpeed = MaxSystemRotationSpeed - MinSystemRotationSpeed;
    const bool EnableRingObjectsRotation = true;
    const FieldDepths FieldDepth = FieldDepths.Near;
    #endregion

    #region Private variables
    System.Random random;
    float timeCounter;
    EntityQuery ringObjectQuery;
    List<(float Angle, float YOverhead, Color Color)> ringLayers;
    List<Entity> ringObjets;
    BeginInitializationEntityCommandBufferSystem entityCommandBufferSystem;
    #endregion

    #region System overrides
    protected override void OnCreate()
    {
        entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();

        Initialize();
    }

    protected override void OnUpdate()
    {
        EntityCommandBuffer.ParallelWriter commandBuffer = entityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();

        
    }
    #endregion

    #region Support
    void Initialize() 
    {
        if (random == null) random = new System.Random();

        if (ringObjets == null) ringObjets = new List<Entity>();

        if (ringLayers == null) ringLayers = new List<(float, float, Color)>
        {
            (0.0f, -4200f, Color.green),
            (0.25f, -3400f, Color.white),
            (0.5f, -2600f, Color.blue),
            (0.75f, -1800f, Color.grey),
            (1.0f, -1000f, Color.yellow),
            (1.25f, -200f, Color.magenta),
            (1.5f, 200f, Color.cyan),
            (1.75f, 1000f, Color.white),
            (2.0f, 1800f, Color.blue),
            (2.25f, 2600f, Color.grey),
            (2.5f, 3400f, Color.yellow),
            (2.75f, 4200f, Color.red)
        };
    }
    
    void CreateRings(List<(float Angle, float YOverhead, Color Color)> ringLayers, float ringSystemA, bool randomizeRingObjectScale = false)
    {
        if (ringLayers == null) return;

        foreach (var ringLayer in ringLayers)
        {
            for (int a = 0; a < 360; a += RingAngleStep) 
            {
                for (int i = 0; i <= NumOfRingsAB + 1; i++) 
                {
                    float scale = randomizeRingObjectScale ? GetRingObjectSize(MinRingObjectScale, MaxRingObjectScale, Distributions.White) : UniformRingObjectScale;

                    AddRingObject(
                        a + ringLayer.Angle, GetRingObjectRadialDistance(i, ringSystemA), scale, ringLayer.YOverhead, 
                        ringLayer.Color, Distributions.White, MinDeviation, MaxDeviation, MinYDeviation, MaxYDeviation);
                }
            }
        }
    }

    int GetSizeAndDistanceMultiplier(FieldDepths fieldDepth) => fieldDepth == FieldDepths.Far ? 100 : 1;

    float GetRingObjectRadialDistance(int ringId, float ringSystemA) => ringSystemA + ringId * RingWidth * GetSizeAndDistanceMultiplier(FieldDepth) / (NumOfRingsAB + 1);

    float GetRingObjectSize(float minSize = 1f, float maxSize = 1000f, Distributions distribution = default) 
    {
        if (random == null) random = new System.Random();

        if (distribution == Distributions.White) return (float)(random.NextDouble() * (maxSize - minSize) + minSize);
        if (distribution == Distributions.Normal) // See https://stackoverflow.com/a/218600
        {
            float u1 = 1.0f - (float)random.NextDouble();
            float u2 = 1.0f - (float)random.NextDouble();
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
            return (maxSize - minSize) / 2 + StdDeviation * randStdNormal;
        }
        if (distribution == Distributions.HalfNormal)
        {
            return Mathf.Sqrt(2f) / (StdDeviation * Mathf.Sqrt(Mathf.PI)) 
                * Mathf.Exp(-Mathf.Pow((float)(random.NextDouble() * (maxSize - minSize) + minSize), 2f) / (2 * Mathf.Pow(StdDeviation, 2)));
        }

        return 0.0f;
    }

    void AddRingObject(float angle, float radius, float scale = 1000f, float yOverhead = 0f, 
        Color color = default, Distributions distribution = default, float minDeviation = -1000f, float maxDeviation = 1000f,
        float minYDeviation = -500f, float maxYDeviation = 500f, bool localRotation = true) 
    {
        
    }
    #endregion
}