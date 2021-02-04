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
    float Angle { get; }

    void SetInitialPosition(
        float radialDistance, float angle, System.Random random = default, Distributions distribution = default, 
        Vector3 overheads = default, float minDeviation = -1000f, float maxDeviation = 1000f);
    void SetInitialRotation(Vector3 rotation);
    void SetSelfRotationSpeed(Vector3 rotationSpeed);
    void SetSystemRotationSpeed(Vector3 rotationSpeed);
    void SetColor(Color color);
    void SetObject(GameObject gameObject);
    void SetUniformScale(float uniformScale);
}