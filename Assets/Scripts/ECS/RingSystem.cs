using Unity.Entities;
using Unity.Burst;
using Unity.Collections;

public class RingSystem : SystemBase
{
    #region Constants
    const float RingWidth = 50000.0f;
    const int NumOfRingsAB = 20;
    const int RingAngleStep = 3;
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
    
    System.Random random;
    float timeCounter;
    EntityQuery ringObjectQuery;

    protected override void OnStartRunning()
    {
        if (random == null) random = new System.Random();
    }

    protected override void OnCreate()
    {
        ringObjectQuery = GetEntityQuery(typeof(RingObject_RotationSpeed), typeof(RingObject_Appearance), typeof(RingObject_Position));
    }

    protected override void OnUpdate()
    {
        ComponentTypeHandle<RingObject_RotationSpeed> rotationSpeedType = GetComponentTypeHandle<RingObject_RotationSpeed>();
        ComponentTypeHandle<RingObject_Appearance> appearanceType = GetComponentTypeHandle<RingObject_Appearance>();
        ComponentTypeHandle<RingObject_Position> positionType = GetComponentTypeHandle<RingObject_Position>();

        RingObjectJob ringObjectJob = new RingObjectJob()
        {
            DeltaTime = Time.DeltaTime,
            RotationSpeedType = rotationSpeedType,
            AppearanceType = appearanceType,
            PositionType = positionType
        };
        
        Dependency = ringObjectJob.ScheduleParallel(ringObjectQuery, 1, Dependency);
    }

    [BurstCompile]
    struct RingObjectJob : IJobEntityBatch
    {
        public float DeltaTime;
        public ComponentTypeHandle<RingObject_RotationSpeed> RotationSpeedType;
        public ComponentTypeHandle<RingObject_Appearance> AppearanceType;
        public ComponentTypeHandle<RingObject_Position> PositionType;
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            NativeArray<RingObject_RotationSpeed> rotationSpeedTypes = batchInChunk.GetNativeArray(RotationSpeedType);
            NativeArray<RingObject_Appearance> appearanceType = batchInChunk.GetNativeArray(AppearanceType);
            NativeArray<RingObject_Position> positionType = batchInChunk.GetNativeArray(PositionType);

            for (var i = 0; i < batchInChunk.Count; i++)
            {
                RingObject_RotationSpeed rotationSpeed = rotationSpeedTypes[i];
                RingObject_Appearance appearance = appearanceType[i];
                RingObject_Position position = positionType[i];

                
            }
        }
    }
}