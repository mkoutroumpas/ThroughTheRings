using UnityEngine;

public interface IRingObject
{
    Vector3 Rotation { get; }
    Vector3 Position { get; }

    void SetPosition(float radialDistance, float angle);

    void SetInitialRotation(Vector3 rotation);
}