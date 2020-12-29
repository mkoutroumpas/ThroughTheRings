using UnityEngine;

public class PlanetRingSystem : MonoBehaviour 
{
    private Vector3 CoordinateSystemZero; 
    private float rA, rB, PlanetRadius = 30000f;
    private const int n = 2, F = 2;

    void Start() 
    {
        CoordinateSystemZero = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        PlanetRadius = gameObject.transform.localScale.z / 2;

        rA = PlanetRadius + 10000;
        rB = rA + 50000;

        Debug.Log($"CoordinateSystemZero = {CoordinateSystemZero}");
        Debug.Log($"rA = {rA}, rB = {rB}");

        AddTestCubes();
    }

    private void AddTestCubes() 
    {
        for (int i = 0; i < 360; i += 5)
        {
            AddTestCube(i, rA, 1000f);
            AddTestCube(i, rB, 1000f);
        }
    }

    private void AddTestCube(int angle, float radius, float scale = 1000f) 
    {
        GameObject cubeArtifact = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubeArtifact.transform.localScale = new Vector3(scale, scale, scale); 
        cubeArtifact.transform.position = new Vector3(radius * Mathf.Sin(angle * Mathf.PI / 180), 
            CoordinateSystemZero.y, CoordinateSystemZero.z - radius * Mathf.Cos(angle * Mathf.PI / 180)); 
    }

    private void AddTestRingSystem()
    {
        for (int f = 0; f < 360; f += F) 
        {
            for (int i = 0; i < n + 1; i++) AddTestCube(f, GetArtifactRadius(i));
        }
    }

    private float GetArtifactRadius(int ringId) => rA + ringId * (rB - rA) / (n + 1);
}