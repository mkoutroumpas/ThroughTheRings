using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RingObject_Appearance : IComponentData
{
    public Color Color;
    public PrimitiveType PrimitiveType;
    public Vector3 Scale;
    public Vector3 InitialRotation;
}
