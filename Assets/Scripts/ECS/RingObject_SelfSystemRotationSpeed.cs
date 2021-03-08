using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RingObject_SelfSystemRotationSpeed : IComponentData
{
    public Vector3 SelfRotationSpeed;
    public Vector3 SystemRotationSpeed;
}
