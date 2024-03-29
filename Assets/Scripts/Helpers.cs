using UnityEngine;

public static class Helpers
{
    public static Vector3 ToCartesian(float angle, float radius, float yOverhead, Vector3 coordinateSystemZero) => 
        new Vector3(radius * Mathf.Cos(angle) + coordinateSystemZero.x, yOverhead, radius * Mathf.Sin(angle) + coordinateSystemZero.z);
}

public static class Settings
{
    public const float RingWidth = 5000.0f;
    public const int RingAngleStep = 3;
    public const int RingAngleMaximum = 360;
    public const int NumOfRingsAB = 60;
    public const float StdDeviation = 0.1f;
    public const float MinRingObjectScale =  0.001f, MaxRingObjectScale = 250.0f;
    public const float MinDeviation = -3500.0f, MaxDeviation = 3500.0f;
    public const float MinYDeviation = -500.0f, MaxYDeviation = 500.0f;
    public const float MaxSelfRotationSpeed = 10.0f, MinSelfRotationSpeed = 0.0f;
    public const float DiffSelfRotSpeed = MaxSelfRotationSpeed - MinSelfRotationSpeed;
    public const float MaxSystemRotationSpeed = 0.005f, MinSystemRotationSpeed = 0.0025f;
    public const float DiffSystemRotSpeed = MaxSystemRotationSpeed - MinSystemRotationSpeed;
    public const bool EnableRingObjectsRotation = true;
    public const FieldDepths FieldDepth = FieldDepths.Near;
    public const Distributions Distribution = Distributions.White;
}