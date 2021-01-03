using System.Collections.Generic;
using UnityEngine;

public class PlanetRingSystem : MonoBehaviour 
{
    Vector3 coordinateSystemZero; 
    List<(float Angle, float YOverhead, Color Color)> ringLayers;
    float rA, rB, planetRadius = 30000f, ringWidth = 50000;
    const int numOfRingsBetween = 20, ringAngleStep = 3;
    const float stdDeviation = 0.1f;
    const int sizeAndDistanceMultiplier = 1; // 1: a unit corresponds to 10 m (near-field objects scaling), 100: a unit corresponds to 1 km (far-field objects scaling).
    const float uniformTestCubeScale = 250f;
    float minCubeScale = 0.01f, maxCubeScale = 1000f;
    static System.Random random;
    int numOfTestArtifacts;

    void Start() 
    {
        numOfTestArtifacts = 0;

        ringLayers = new List<(float, float, Color)>
        {
            (0.0f, -2000f, Color.green),
            (0.5f, -1200f, Color.red),
            (1.0f, -400f, Color.blue),
            (1.5f, 400f, Color.grey),
            (2.0f, 1200f, Color.yellow),
            (2.5f, 2000f, Color.white)
        };

        coordinateSystemZero = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        planetRadius = gameObject.transform.localScale.z / 2;

        rA = planetRadius + 10000;
        rB = rA + ringWidth;

        Debug.Log($"CoordinateSystemZero = {coordinateSystemZero}");
        Debug.Log($"rA = {rA}, rB = {rB}");

        foreach (var ringLayer in ringLayers) AddTestRingSystem(ringLayer.Angle, ringLayer.YOverhead, ringLayer.Color, true);

        Debug.Log($"numOfTestArtifacts = {numOfTestArtifacts}");
    }

    void AddTestRingSystem(float startAngle = 0f, float yOverhead = 0f, Color color = default, bool randomize = false)
    {
        for (int a = 0; a < 360; a += ringAngleStep) 
        {
            for (int i = 0; i <= numOfRingsBetween + 1; i++) 
            {
                float scale = randomize ? GetArtifactSize(minCubeScale, maxCubeScale, Distributions.White) : uniformTestCubeScale;

                AddTestCube(a + startAngle, GetArtifactRadialDistance(i), scale, yOverhead, color);
            }
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
        
        numOfTestArtifacts++;
    }

    float GetArtifactRadialDistance(int ringId) => rA + ringId * ringWidth * sizeAndDistanceMultiplier / (numOfRingsBetween + 1);

    float GetArtifactSize(float minSize = 1f, float maxSize = 1000f, Distributions distribution = default) 
    {
        if (random == null) random = new System.Random();

        if (distribution == Distributions.White) return (float)(random.NextDouble() * (maxSize - minSize) + minSize);
        if (distribution == Distributions.Normal) // See https://stackoverflow.com/a/218600
        {
            float u1 = 1.0f - (float)random.NextDouble();
            float u2 = 1.0f - (float)random.NextDouble();
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
            return (maxSize - minSize) / 2 + stdDeviation * randStdNormal;
        }
        if (distribution == Distributions.HalfNormal)
        {
            return Mathf.Sqrt(2f) / (stdDeviation * Mathf.Sqrt(Mathf.PI)) 
                * Mathf.Exp(-Mathf.Pow((float)(random.NextDouble() * (maxSize - minSize) + minSize), 2f) / (2 * Mathf.Pow(stdDeviation, 2)));
        }

        return 0f;
    }

    enum Distributions
    {
        White,
        Normal,
        HalfNormal
    }
}