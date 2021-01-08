using UnityEngine;
public class RingObject : MonoBehaviour, IRingObject
{
    public Vector3 Position { get; private set; }
    public Vector3 Rotation { get; private set; }
    public Vector3 RotationSpeed { get; private set; }
    public Color Color{ get; private set; }

    public void SetPosition(float radialDistance, float angle)
    {
        // TODO: Trigonometrically set position coordinates.
    }

    public void SetInitialRotation(Vector3 rotation)
    {
        if (rotation == null) return;
        this.Rotation = rotation;
    }

    public void SetRotationSpeed(Vector3 rotationSpeed)
    {
        if (rotationSpeed == null) return;
        this.RotationSpeed = rotationSpeed;

        // TODO: Also implement ability to change rotation speed 'on-the-fly' (guess will be handled by Unity's thread ...)
    }

    public void SetColor(Color color)
    {
        if (color == null) return;
        this.Color = color;
    }
}