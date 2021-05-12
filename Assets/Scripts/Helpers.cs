using UnityEngine;

public static class Helpers
{
    public static Vector3 ToCartesian(float angle, float radius, float yOverhead, Vector3 coordinateSystemZero) => 
        new Vector3(radius * Mathf.Cos(angle) + coordinateSystemZero.x, yOverhead, radius * Mathf.Sin(angle) + coordinateSystemZero.z);
}

public class Settings
{

}