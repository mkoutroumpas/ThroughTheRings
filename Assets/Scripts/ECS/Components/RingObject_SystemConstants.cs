using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RingObject_SystemConstants : IComponentData
{
    public Vector3 CoordinateSystemZero;
    public float PlanetRadius;
    public float RingWidth;
    public int NumOfRingsAB;
    public int RingAngleStep;
    public float MinRingObjectScale, MaxRingObjectScale;
    public float MinYDeviation, MaxYDeviation;
    public bool EnableRingObjectsRotation;
    public FieldDepths FieldDepth;
}
