using UnityEngine;

public class PlanetRingSystem : MonoBehaviour 
{
    Vector3 coordinateSystemZero; 
    float rA, rB, planetRadius = 30000f, ringRadius = 50000;
    const int numOfRingsBetween = 2, ringAngleStep = 2;

    void Start() 
    {
        coordinateSystemZero = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        planetRadius = gameObject.transform.localScale.z / 2;

        rA = planetRadius + 10000;
        rB = rA + ringRadius;

        Debug.Log($"CoordinateSystemZero = {coordinateSystemZero}");
        Debug.Log($"rA = {rA}, rB = {rB}");

        AddTestRingSystem();
    }

    void AddTestCube(int angle, float radius, float scale = 1000f) 
    {
        GameObject artifact = GameObject.CreatePrimitive(PrimitiveType.Cube);
        artifact.transform.localScale = new Vector3(scale, scale, scale); 
        artifact.transform.position = new Vector3(radius * Mathf.Sin(angle * Mathf.PI / 180), 
            coordinateSystemZero.y, coordinateSystemZero.z - radius * Mathf.Cos(angle * Mathf.PI / 180)); 
    }

    void AddTestRingSystem()
    {
        for (int a = 0; a < 360; a += ringAngleStep) 
        {
            for (int i = 0; i <= numOfRingsBetween + 1; i++) AddTestCube(a, GetArtifactRadius(i), 100f);
        }
    }

    float GetArtifactRadius(int ringId) => rA + ringId * ringRadius / (numOfRingsBetween + 1);
}