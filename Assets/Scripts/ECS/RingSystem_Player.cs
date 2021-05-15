using Unity.Entities;
using Unity.Burst;
using Unity.Collections;

public class RingSystem_Player : SystemBase
{
    #region Private variables
    EntityQuery ringObjectQuery;
    #endregion

    #region System overrides
    protected override void OnCreate()
    {
        ringObjectQuery = GetEntityQuery(typeof(RingObject_Position), 
            ComponentType.ReadOnly<RingObject_RotationSpeed>(),
            ComponentType.ReadOnly<RingObject_Appearance>());
    }

    protected override void OnUpdate()
    {
        ComponentTypeHandle<RingObject_Position> ringObjectPosition = GetComponentTypeHandle<RingObject_Position>();
        ComponentTypeHandle<RingObject_RotationSpeed> ringObjectRotationSpeed = GetComponentTypeHandle<RingObject_RotationSpeed>(true);
        ComponentTypeHandle<RingObject_Appearance> ringObjectAppearance = GetComponentTypeHandle<RingObject_Appearance>(true);

        MainJob mainJob = new MainJob()
        {
            PositionTypeHandle = ringObjectPosition,
            RotationSpeedTypeHandle = ringObjectRotationSpeed,
            AppearanceTypeHandle = ringObjectAppearance,
            DeltaTime = Time.DeltaTime
        };
    }
    #endregion

    #region Jobs
    [BurstCompile]
    [RequireComponentTag(typeof(RingObject_Position), typeof(RingObject_RotationSpeed), typeof(RingObject_Appearance))]
    struct MainJob : IJobEntityBatch
    {
        public float DeltaTime;
        public ComponentTypeHandle<RingObject_Position> PositionTypeHandle;
        [ReadOnly] public ComponentTypeHandle<RingObject_RotationSpeed> RotationSpeedTypeHandle;
        [ReadOnly] public ComponentTypeHandle<RingObject_Appearance> AppearanceTypeHandle;
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            NativeArray<RingObject_Position> chunkPositions = batchInChunk.GetNativeArray(PositionTypeHandle);
            NativeArray<RingObject_RotationSpeed> chunkRotationSpeeds = batchInChunk.GetNativeArray(RotationSpeedTypeHandle);
            NativeArray<RingObject_Appearance> chunkAppearances = batchInChunk.GetNativeArray(AppearanceTypeHandle);

            for (var i = 0; i < batchInChunk.Count; i++)
            {
                RingObject_Position position = chunkPositions[i];
                RingObject_RotationSpeed rotationSpeed = chunkRotationSpeeds[i];
                RingObject_Appearance appearance = chunkAppearances[i];





            }

        }
    }

    #endregion
}