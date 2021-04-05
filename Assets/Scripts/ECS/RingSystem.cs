using Unity.Entities;
using Unity.Burst;
using Unity.Collections;

public class RingSystem : SystemBase
{
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