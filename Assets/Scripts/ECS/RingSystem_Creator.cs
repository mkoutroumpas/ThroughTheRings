using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Collections;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class RingSystem_Creator : SystemBase
{
    #region Constants
    const float RingWidth = 50000.0f;
    const int NumOfRingsAB = 20;
    const int RingAngleStep = 3;
    const float StdDeviation = 0.1f;
    const float MinRingObjectScale =  0.001f, MaxRingObjectScale = 250.0f;
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
    EntityQuery ringObjectQuery;
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

        NativeArray<RingLayerData> ringLayers = new NativeArray<RingLayerData>(12, Allocator.Temp)
        {
            [0] = new RingLayerData() { Angle = 0.0f, YOverhead = -4200f, Color = Color.green },
            [1] = new RingLayerData() { Angle = 0.25f, YOverhead = -3400f, Color = Color.white },
            [2] = new RingLayerData() { Angle = 0.5f, YOverhead = -2600f, Color = Color.blue }, 
            [3] = new RingLayerData() { Angle = 0.75f, YOverhead = -1800f, Color = Color.grey }, 
            [4] = new RingLayerData() { Angle = 1.0f, YOverhead = -1000f, Color = Color.yellow }, 
            [5] = new RingLayerData() { Angle = 1.25f, YOverhead = -200f, Color = Color.magenta }, 
            [6] = new RingLayerData() { Angle = 1.5f, YOverhead = 200f, Color = Color.cyan }, 
            [7] = new RingLayerData() { Angle = 1.75f, YOverhead = 1000f, Color = Color.white }, 
            [8] = new RingLayerData() { Angle = 2.0f, YOverhead = 1800f, Color = Color.blue }, 
            [9] = new RingLayerData() { Angle = 2.25f, YOverhead = 2600f, Color = Color.grey }, 
            [10] = new RingLayerData() { Angle = 2.5f, YOverhead = 3400f, Color = Color.yellow }, 
            [11] = new RingLayerData() { Angle = 2.75f, YOverhead = 4200f, Color = Color.red }
        };

        Entities
            .WithName("SpawnerSystem_FromEntity")
            .WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
            .ForEach((Entity entity, int entityInQueryIndex, ref RingObject_Appearance appearance, ref RingObject_Position position, ref RingObject_RotationSpeed rotationSpeed, in RingObject_SystemData systemData, in LocalToWorld location) =>
            {
                foreach (var ringLayer in ringLayers)
                {
                    for (int a = 0; a < 360; a += RingAngleStep) 
                    {
                        for (int i = 0; i <= NumOfRingsAB + 1; i++) 
                        {
                            float ringObjectSize = GetRingObjectSize(MinRingObjectScale, MaxRingObjectScale, Distributions.White);

                            // AddRingObject(
                            //     a + ringLayer.Angle, GetRingObjectRadialDistance(i, systemData.PlanetRadius + 10000.0f), scale, ringLayer.YOverhead, 
                            //     ringLayer.Color, Distributions.White, MinDeviation, MaxDeviation, MinYDeviation, MaxYDeviation);
                        }
                    }
                }

                commandBuffer.DestroyEntity(entityInQueryIndex, entity);
            }).ScheduleParallel();

        entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
    }
    #endregion

    #region Support
    struct RingLayerData
    {
        public float Angle { get; set; }
        public float YOverhead { get; set; }
        public Color Color { get; set; }
    }
    static int GetSizeAndDistanceMultiplier(FieldDepths fieldDepth) => fieldDepth == FieldDepths.Far ? 100 : 1; 

    static float GetRingObjectRadialDistance(int ringId, float ringSystemA) => ringSystemA + ringId * RingWidth * GetSizeAndDistanceMultiplier(FieldDepth) / (NumOfRingsAB + 1);

    static float GetRingObjectSize(float minSize = 1f, float maxSize = 1000f, Distributions distribution = default) 
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

    static void AddRingObject(float angle, float radius, float scale = 1000f, float yOverhead = 0f, 
        Color color = default, Distributions distribution = default, float minDeviation = -1000f, float maxDeviation = 1000f,
        float minYDeviation = -500f, float maxYDeviation = 500f, bool localRotation = true) 
    {
        
    }
    #endregion
}