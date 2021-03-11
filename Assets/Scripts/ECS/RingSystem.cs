using Unity.Entities;
using Unity.Burst;

public class RingSystem : SystemBase
{
    EntityQuery ringObjectQuery;

    protected override void OnCreate()
    {
        ringObjectQuery = GetEntityQuery(typeof(RingObject_RotationSpeed), typeof(RingObject_Appearance), typeof(RingObject_Position), ComponentType.ReadOnly<RingSystem_Entity>());
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
            
        }
    }

    [BurstCompile]
    struct RotationSpeedJob : IJobEntityBatch
    {
        public float DeltaTime;
        public ComponentTypeHandle<RingObject_RotationSpeed> RotationSpeedType;
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            
        }
    }

    [BurstCompile]
    struct AppearanceJob : IJobEntityBatch
    {
        public ComponentTypeHandle<RingObject_Appearance> AppearanceType;
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            
        }
    }

    [BurstCompile]
    struct PositionJob : IJobEntityBatch
    {
        public float DeltaTime;
        public ComponentTypeHandle<RingObject_Position> PositionType;
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            
        }
    }
}