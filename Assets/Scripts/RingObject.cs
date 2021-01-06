using UnityEngine;
public class RingObject : IRingObject
{
    public Vector3 Position { get; private set; }
    public Vector3 Rotation { get; private set; }

    public void SetPosition(float radialDistance, float angle)
    {
        // TODO: Trigonometrically set position coordinates.
    }

    public void SetInitialRotation(Vector3 rotation)
    {
        if (rotation == null) return;
        this.Rotation = rotation;
    }
}