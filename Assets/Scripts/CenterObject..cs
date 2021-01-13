using UnityEngine;
public class CenterObject : RingObject
{
    public CenterObject(Vector3 initialPosition, Vector3 coordSystemZero, PrimitiveType primitiveType = PrimitiveType.Cube, float uniformScale = 1f)
        : base (initialPosition, coordSystemZero, PrimitiveType.Sphere, uniformScale)
    {

    }
}