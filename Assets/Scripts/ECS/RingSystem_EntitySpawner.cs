using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using System.Collections.Generic;

public class RingSystem_EntitySpawner : MonoBehaviour
{
    #region Public properties
    public GameObject RingObjectPrefab;
    #endregion

    #region Constants
    const float RingWidth = 50000.0f;
    const int RingAngleStep = 3;
    const int NumOfRingsAB = 20;
    const float StdDeviation = 0.1f;
    const float MinRingObjectScale =  0.001f, MaxRingObjectScale = 250.0f;
    const float MinDeviation = -5000.0f, MaxDeviation = 5000.0f;
    const float MinYDeviation = -500.0f, MaxYDeviation = 500.0f;
    const FieldDepths FieldDepth = FieldDepths.Near;
    const Distributions Distribution = Distributions.White;
    #endregion

    #region Private variables
    List<(float Angle, float YOverhead, float RingA, Color Color)> _RingLayers = new List<(float, float, float, Color)>
    {
        (0.0f, -4200f, 0f, Color.green),
        (0.25f, -3400f, 0f, Color.white),
        (0.5f, -2600f, 0f, Color.blue),
        (0.75f, -1800f, 0f, Color.grey),
        (1.0f, -1000f, 0f, Color.yellow),
        (1.25f, -200f, 0f, Color.magenta),
        (1.5f, 200f, 0f, Color.cyan),
        (1.75f, 1000f, 0f, Color.white),
        (2.0f, 1800f, 0f, Color.blue),
        (2.25f, 2600f, 0f, Color.grey),
        (2.5f, 3400f, 0f, Color.yellow),
        (2.75f, 4200f, 0f, Color.red)
    };
    #endregion

    void Start()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(RingObjectPrefab, settings);



        var instance = entityManager.Instantiate(prefab);
        var position = transform.TransformPoint(new Vector3(0, 0, 0));
        entityManager.SetComponentData(instance, new Translation { Value = position });
    }
    int GetSizeAndDistanceMultiplier(FieldDepths fieldDepth) => fieldDepth == FieldDepths.Far ? 100 : 1; 
    float GetRingObjectRadialDistance(int ringId, float ringSystemA) => ringSystemA + ringId * RingWidth * GetSizeAndDistanceMultiplier(FieldDepth) / (NumOfRingsAB + 1);
    float GetRingObjectSize(float minSize = 1f, float maxSize = 1000f, Distributions distribution = default) 
    {
        if (distribution == Distributions.White) return (float)(Random.Range(0.0f, 1.0f) * (maxSize - minSize) + minSize);
        if (distribution == Distributions.Normal) // See https://stackoverflow.com/a/218600
        {
            float u1 = 1.0f - Random.Range(0.0f, 1.0f);
            float u2 = 1.0f - Random.Range(0.0f, 1.0f);
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);

            return (maxSize - minSize) / 2 + StdDeviation * randStdNormal;
        }
        if (distribution == Distributions.HalfNormal) return Mathf.Sqrt(2f) / (StdDeviation * Mathf.Sqrt(Mathf.PI)) 
            * Mathf.Exp(-Mathf.Pow((float)(Random.Range(0.0f, 1.0f) * (maxSize - minSize) + minSize), 2f) / (2 * Mathf.Pow(StdDeviation, 2)));

        return 0.0f;
    }
    void CreateRings(List<(float Angle, float YOverhead, float RingA, Color Color)> ringLayers)
    {
        if (ringLayers == null) return;

        foreach (var ringLayer in ringLayers)
        {
            for (int a = 0; a < 360; a += RingAngleStep) 
            {
                for (int i = 0; i <= NumOfRingsAB + 1; i++) 
                {
                    AddRingObject(
                        a + ringLayer.Angle, GetRingObjectRadialDistance(i, ringLayer.RingA), GetRingObjectSize(MinRingObjectScale, MaxRingObjectScale, Distributions.White), 
                        ringLayer.YOverhead, ringLayer.Color, Distributions.White, MinDeviation, MaxDeviation, MinYDeviation, MaxYDeviation);
                }
            }
        }
    }
    void AddRingObject(float angle, float radius, float scale = 1000f, float yOverhead = 0f, 
        Color color = default, Distributions distribution = default, float minDeviation = -1000f, float maxDeviation = 1000f,
        float minYDeviation = -500f, float maxYDeviation = 500f) 
    {

    }
}