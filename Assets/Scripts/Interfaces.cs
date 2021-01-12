using UnityEngine;

public interface IRingObject
{
    Vector3 Rotation { get; }
    Vector3 Position { get; }
    Vector3 RotationSpeed { get; }
    Color Color{ get; }
    GameObject Object { get; }
    Vector3 Scale { get; }

    void SetPosition(
        float radialDistance, float angle, System.Random random = default, Distributions distribution = default, 
        Vector3 overheads = default, float minDeviation = -1000f, float maxDeviation = 1000f);
    void SetInitialRotation(Vector3 rotation);
    void SetRotationSpeed(Vector3 rotationSpeed);
    void SetColor(Color color);
    void SetObject(GameObject gameObject);
    void SetUniformScale(float uniformScale);
}