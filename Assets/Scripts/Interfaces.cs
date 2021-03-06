using UnityEngine;

public interface IRingSystemObject
{
    Vector3 Rotation { get; }
    Vector3 Position { get; }
    Vector3 SelfRotationSpeed { get; }
    Vector3 SystemRotationSpeed { get; }
    Color Color{ get; }
    GameObject Object { get; }
    Vector3 Scale { get; }
    float SystemInitialAngle { get; }
    float SystemInitialRadius { get; }

    void SetInitialRotation(Vector3 rotation);
    void SetSelfRotationSpeed(Vector3 rotationSpeed);
    void SetSystemRotationSpeed(Vector3 rotationSpeed);
    void SetColor(Color color);
    void SetObject(GameObject gameObject);
    void SetUniformScale(float uniformScale);
    void SetSystemInitialAngle(float angle);
    void SetSystemInitialRadius(float radius);
}