using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RingObject_RotationSpeed : IComponentData
{
    public Vector3 Self;
    public Vector3 System;
}
