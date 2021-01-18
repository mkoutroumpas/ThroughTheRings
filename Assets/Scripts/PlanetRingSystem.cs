using System.Collections.Generic;
using UnityEngine;

public class PlanetRingSystem : MonoBehaviour 
{
    #region Enums
    enum FieldDepths
    {
        Near,
        Far
    }
    #endregion

    #region Private and const variables
    Vector3 _coordinateSystemZero;
    Vector3 _rotationSpeeds;
    List<(float Angle, float YOverhead, Color Color)> _ringLayers;
    List<RingObject> _ringObjects;
    float _ringA, _ringB;
    float _planetRadius = 30000f;
    int SizeAndDistanceMultiplier = 1; // 1: a unit corresponds to 10 m (near-field objects scaling), 100: a unit corresponds to 1 km (far-field objects scaling).
    const float RingWidth = 50000;
    const int NumOfRingsAB = 20, RingAngleStep = 3;
    const float StdDeviation = 0.1f;
    const float UniformTestCubeScale = 250f;
    const float MinCubeScale = 0.001f, MaxCubeScale = 250f;
    const float MinDeviation = -5000f, MaxDeviation = 5000f;
    static System.Random random;
    #endregion

    void Start() 
    {
        _ringObjects = new List<RingObject>();
        
        _ringLayers = new List<(float, float, Color)>
        {
            (0.0f, -4200f, Color.green),
            (0.25f, -3400f, Color.white),
            (0.5f, -2600f, Color.blue),
            (0.75f, -1800f, Color.grey),
            (1.0f, -1000f, Color.yellow),
            (1.25f, -200f, Color.magenta),
            (1.5f, 200f, Color.cyan),
            (1.75f, 1000f, Color.white),
            (2.0f, 1800f, Color.blue),
            (2.25f, 2600f, Color.grey),
            (2.5f, 3400f, Color.yellow),
            (2.75f, 4200f, Color.red)
        };

        _rotationSpeeds = new Vector3(5f, 5f, 5f);

        _coordinateSystemZero = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        _planetRadius = gameObject.transform.localScale.z / 2;

        _ringA = _planetRadius + 10000;
        _ringB = _ringA + RingWidth;

        Debug.Log($"CoordinateSystemZero = {_coordinateSystemZero}");
        Debug.Log($"rA = {_ringA}, rB = {_ringB}");

        // AddCenterPlanet(this._planetRadius);

        CreateRings(this._ringLayers, true);

        Debug.Log($"numOfTestArtifacts = {this._ringObjects.Count}");
    }

    void Update()
    {
        if (this._ringObjects.Count > 0)
        {
            foreach (RingObject ringObject in this._ringObjects) ringObject.Object.transform.Rotate(this._rotationSpeeds * Time.deltaTime, Space.Self);
        }
    }

    void AddCenterPlanet(float radius = 0f)
    {
        CenterPlanet centerPlanet = new CenterPlanet(_coordinateSystemZero, _coordinateSystemZero, PrimitiveType.Sphere, radius);
        centerPlanet.SetColor(Color.gray);
    }

    void CreateRings(List<(float Angle, float YOverhead, Color Color)> ringLayers, bool randomizeRingObjectScale = false)
    {
        if (ringLayers == null) return;

        foreach (var ringLayer in ringLayers)
        {
            for (int a = 0; a < 360; a += RingAngleStep) 
            {
                for (int i = 0; i <= NumOfRingsAB + 1; i++) 
                {
                    float scale = randomizeRingObjectScale ? GetRingObjectSize(MinCubeScale, MaxCubeScale, Distributions.White) : UniformTestCubeScale;

                    AddRingObject(
                        a + ringLayer.Angle, GetArtifactRadialDistance(i), scale, ringLayer.YOverhead, ringLayer.Color, Distributions.White, MinDeviation, MaxDeviation);
                }
            }
        }
    }

    void AddRingObject(float angle, float radius, float scale = 1000f, float yOverhead = 0f, 
        Color color = default, Distributions distribution = default, float minDeviation = -1000f, float maxDeviation = 1000f,
        bool localRotation = true) 
    {
        RingObject ringObject = new RingObject(default, _coordinateSystemZero, PrimitiveType.Cube, scale);
        ringObject.SetColor(color);
        ringObject.SetPosition(radius, angle, random, Distributions.White, new Vector3(0f, yOverhead, 0f), minDeviation, maxDeviation);
        
        if (localRotation) ringObject.SetInitialRotation(new Vector3((float)random.NextDouble() * 360, (float)random.NextDouble() * 360, (float)random.NextDouble() * 360));

        this._ringObjects.Add(ringObject);
    }

    float GetArtifactRadialDistance(int ringId) => _ringA + ringId * RingWidth * SizeAndDistanceMultiplier / (NumOfRingsAB + 1);

    float GetRingObjectSize(float minSize = 1f, float maxSize = 1000f, Distributions distribution = default) 
    {
        if (random == null) random = new System.Random();

        if (distribution == Distributions.White) return (float)(random.NextDouble() * (maxSize - minSize) + minSize);
        if (distribution == Distributions.Normal) // See https://stackoverflow.com/a/218600
        {
            float u1 = 1.0f - (float)random.NextDouble();
            float u2 = 1.0f - (float)random.NextDouble();
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
            return (maxSize - minSize) / 2 + StdDeviation * randStdNormal;
        }
        if (distribution == Distributions.HalfNormal)
        {
            return Mathf.Sqrt(2f) / (StdDeviation * Mathf.Sqrt(Mathf.PI)) 
                * Mathf.Exp(-Mathf.Pow((float)(random.NextDouble() * (maxSize - minSize) + minSize), 2f) / (2 * Mathf.Pow(StdDeviation, 2)));
        }

        return 0f;
    }
}