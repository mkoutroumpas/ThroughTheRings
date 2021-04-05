using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RingObject_SystemData : IComponentData
{
    public Vector3 CoordinateSystemZero;
    public float PlanetRadius;
}
