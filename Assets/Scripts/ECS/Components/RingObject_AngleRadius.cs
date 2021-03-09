using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RingObject_Position : IComponentData
{
    public float Angle;
    public float Radius;
}
