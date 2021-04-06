using System.Collections.Generic;
using UnityEngine;
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

    #region Private variables
    System.Random random;
    float timeCounter;
    EntityQuery ringObjectQuery;
    List<(float Angle, float YOverhead, Color Color)> ringLayers;
    List<Entity> ringObjets;
    #endregion

    #region System overrides
    protected override void OnStartRunning()
    {
        
    }

    protected override void OnCreate()
    {
        CreateObjectHolders();

        ringObjectQuery = GetEntityQuery(typeof(RingObject_RotationSpeed), typeof(RingObject_Appearance), typeof(RingObject_Position), typeof(RingObject_SystemData));
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
    #endregion

    #region Support
    void CreateObjectHolders() 
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
    #endregion

    [BurstCompile]
    struct RingObjectJob : IJobEntityBatch
    {
        public float DeltaTime;
        public ComponentTypeHandle<RingObject_RotationSpeed> RotationSpeedType;
        public ComponentTypeHandle<RingObject_Appearance> AppearanceType;
        public ComponentTypeHandle<RingObject_Position> PositionType;
        public ComponentTypeHandle<RingObject_SystemData> SystemData;
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
        {
            NativeArray<RingObject_RotationSpeed> rotationSpeedTypes = batchInChunk.GetNativeArray(RotationSpeedType);
            NativeArray<RingObject_Appearance> appearanceType = batchInChunk.GetNativeArray(AppearanceType);
            NativeArray<RingObject_Position> positionType = batchInChunk.GetNativeArray(PositionType);
            NativeArray<RingObject_SystemData> systemData = batchInChunk.GetNativeArray(SystemData);

            for (var i = 0; i < batchInChunk.Count; i++)
            {
                RingObject_RotationSpeed rotationSpeed = rotationSpeedTypes[i];
                RingObject_Appearance appearance = appearanceType[i];
                RingObject_Position position = positionType[i];
                RingObject_SystemData sData = systemData[i];

                
            }
        }
    }
}