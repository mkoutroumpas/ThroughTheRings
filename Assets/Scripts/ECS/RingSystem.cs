using Unity.Entities;
using Unity.Burst;

public class RingSystem : SystemBase
{
    EntityQuery rotationSpeedQuery;
    EntityQuery appearanceQuery;
    EntityQuery positionQuery;

    protected override void OnCreate()
    {
        rotationSpeedQuery = GetEntityQuery(typeof(RingObject_RotationSpeed), ComponentType.ReadOnly<RingSystem_Entity>());
        appearanceQuery = GetEntityQuery(typeof(RingObject_Appearance), ComponentType.ReadOnly<RingSystem_Entity>());
        positionQuery = GetEntityQuery(typeof(RingObject_Position), ComponentType.ReadOnly<RingSystem_Entity>());
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

        RotationSpeedJob rotationSpeedJob = new RotationSpeedJob()
        {
            DeltaTime = Time.DeltaTime,
            RotationSpeedType = rotationSpeedType
        };

        AppearanceJob appearanceJob = new AppearanceJob()
        {
            AppearanceType = appearanceType
        };

        PositionJob positionJob = new PositionJob()
        {
            DeltaTime = Time.DeltaTime,
            PositionType = positionType
        };

        Dependency = rotationSpeedJob.ScheduleParallel(rotationSpeedQuery, 1, Dependency);
        Dependency = appearanceJob.ScheduleParallel(appearanceQuery, 1, Dependency);
        Dependency = positionJob.ScheduleParallel(positionQuery, 1, Dependency);
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