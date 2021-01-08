using UnityEngine;

public interface IRingObject
{
    Vector3 Rotation { get; }
    Vector3 Position { get; }
    Vector3 RotationSpeed { get; }
    Color Color{ get; }

    void SetPosition(float radialDistance, float angle);

    void SetInitialRotation(Vector3 rotation);
    void SetRotationSpeed(Vector3 rotationSpeed);
    void SetColor(Color color);
}