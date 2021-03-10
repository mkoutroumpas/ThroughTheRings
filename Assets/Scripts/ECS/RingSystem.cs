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