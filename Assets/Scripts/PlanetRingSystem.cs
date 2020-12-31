using System.Collections.Generic;
using UnityEngine;

public class PlanetRingSystem : MonoBehaviour 
{
    Vector3 coordinateSystemZero; 
    List<(float Angle, float YOverhead, Color Color)> ringLayers;
    float rA, rB, planetRadius = 30000f, ringRadius = 50000;
    const int numOfRingsBetween = 10, ringAngleStep = 6;
    const int sizeAndDistanceMultiplier = 1; // Near-field objects scaling. 1: 1 unit corresponds to 10 m, 100: 1 unit corresponds to 1 km.
    const float testCubeScale = 250f;

    void Start() 
    {
        ringLayers = new List<(float, float, Color)>
        {
            (0f, -1000f, Color.green),
            (2f, 0f, Color.red),
            (4f, 1000f, Color.blue)
        };

        coordinateSystemZero = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        planetRadius = gameObject.transform.localScale.z / 2;

        rA = planetRadius + 10000;
        rB = rA + ringRadius;

        Debug.Log($"CoordinateSystemZero = {coordinateSystemZero}");
        Debug.Log($"rA = {rA}, rB = {rB}");

        foreach (var ringLayer in ringLayers) AddTestRingSystem(ringLayer.Angle, ringLayer.YOverhead, ringLayer.Color);
    }

    void AddTestRingSystem(float startAngle = 0f, float yOverhead = 0f, Color color = default)
    {
        for (int a = 0; a < 360; a += ringAngleStep) 
        {
            for (int i = 0; i <= numOfRingsBetween + 1; i++) AddTestCube(a + startAngle, GetArtifactRadialDistance(i), testCubeScale, yOverhead, color);
        }
    }

    void AddTestCube(float angle, float radius, float scale = 1000f, float yOverhead = 0f, Color color = default) 
    {
        GameObject artifact = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer artifactRenderer = artifact.GetComponent<Renderer>();
        artifactRenderer?.material.SetColor("_Color", color);
        artifact.transform.localScale = new Vector3(scale, scale, scale); 
        artifact.transform.position = new Vector3(radius * Mathf.Sin(angle * Mathf.PI / 180), 
            coordinateSystemZero.y + yOverhead, coordinateSystemZero.z - radius * Mathf.Cos(angle * Mathf.PI / 180)); 
    }

    float GetArtifactRadialDistance(int ringId) => rA + ringId * ringRadius * sizeAndDistanceMultiplier / (numOfRingsBetween + 1);
}