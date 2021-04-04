using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct RingObject_PlanetCharacteristics : IComponentData
{
    public Vector3 CoordinateSystemZero;
    public float Radius;
}
