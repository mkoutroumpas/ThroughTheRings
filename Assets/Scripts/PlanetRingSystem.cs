using UnityEngine;

public class PlanetRingSystem : MonoBehaviour 
{
    private Vector3 CoordinateSystemZero; 
    private float rA, rB, PlanetRadius = 30000f, RingRadius = 50000;
    private const int numOfRingsBetween = 2, ringAngleStep = 2;

    void Start() 
    {
        CoordinateSystemZero = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        PlanetRadius = gameObject.transform.localScale.z / 2;

        rA = PlanetRadius + 10000;
        rB = rA + RingRadius;

        Debug.Log($"CoordinateSystemZero = {CoordinateSystemZero}");
        Debug.Log($"rA = {rA}, rB = {rB}");

        AddTestRingSystem();
    }

    private void AddTestCubes() 
    {
        for (int i = 0; i < 360; i += 5)
        {
            AddTestCube(i, rA, 100f);
            AddTestCube(i, rB, 100f);
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
        for (int a = 0; a < 360; a += ringAngleStep) 
        {
            for (int i = 0; i <= numOfRingsBetween + 1; i++) AddTestCube(a, GetArtifactRadius(i), 100f);
        }
    }

    private float GetArtifactRadius(int ringId) => rA + ringId * RingRadius / (numOfRingsBetween + 1);
}