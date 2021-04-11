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
    #endregion
}