using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RingObject_SystemConstants : IComponentData
{
    public Vector3 CoordinateSystemZero;
    public float PlanetRadius;
    public float RingWidth;
}
