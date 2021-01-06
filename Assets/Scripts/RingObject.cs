using UnityEngine;
public class RingObject : IRingObject
{
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }

    public void SetPosition(float radialDistance, float angle)
    {
        // TODO: Trigonometrically set position coordinates.
    }

    public void SetInitialRotation(Vector3 rotation)
    {
        // TODO: Set initial rotation.
    }
}