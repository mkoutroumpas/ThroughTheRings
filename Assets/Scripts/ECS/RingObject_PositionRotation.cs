using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RingObject_PositionRotation : IComponentData
{
    public Vector3 Position;
    public Vector3 Rotation;
}
