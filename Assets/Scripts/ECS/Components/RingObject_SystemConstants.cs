using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RingObject_SystemConstants : IComponentData
{
    public const float RingWidth = 50000.0f;
    public const int NumOfRingsAB = 20;
    public const int RingAngleStep = 3;
    public const float MinRingObjectScale =  0.001f, MaxRingObjectScale = 250.0f;
    public const float MinDeviation = -5000.0f, MaxDeviation = 5000.0f;
    public const float MinYDeviation = -500.0f, MaxYDeviation = 500.0f;
    public const float MaxSelfRotationSpeed = 10.0f, MinSelfRotationSpeed = 0.0f;
    public const float DiffSelfRotSpeed = MaxSelfRotationSpeed - MinSelfRotationSpeed;
    public const float MaxSystemRotationSpeed = 0.005f, MinSystemRotationSpeed = 0.0025f;
    public const float DiffSystemRotSpeed = MaxSystemRotationSpeed - MinSystemRotationSpeed;
    public const bool EnableRingObjectsRotation = true;
    public const FieldDepths FieldDepth = FieldDepths.Near;
}
