using Unity.Entities;
using UnityEngine;
using Unity.Collections;
using Unity.Transforms;
using Unity.Rendering;
using System.Collections.Generic;

public class RingSystem_EntitySpawner : MonoBehaviour
{
    #region Private variables
    GameObject[] _prefabIcoSpheres;
    EntityManager _entityManager;
    Entity _prefabEntity;
    Vector3 _coordinateSystemZero;
    float _planetRadius;
    System.Random random;
    List<(float Angle, float YOverhead, float RingA, Color Color)> _ringLayers;
    #endregion

    void Start()
    {
        _coordinateSystemZero = new Vector3(
            gameObject.transform.position.x, 
            gameObject.transform.position.y, 
            gameObject.transform.position.z);

        _planetRadius = 3000f; // gameObject.transform.localScale.z / 2;

        float ringStart = _planetRadius + 6000f;

        _ringLayers = new List<(float, float, float, Color)>
        {
            (0.0f, -425f, ringStart, Color.green),
            (0.25f, -350f, ringStart, Color.white),
            (0.5f, -275f, ringStart, Color.blue),
            (0.75f, -200f, ringStart, Color.grey),
            (1.0f, -125f, ringStart, Color.yellow),
            (1.25f, -50f, ringStart, Color.magenta),
            (1.5f, 50f, ringStart, Color.cyan),
            (1.75f, 125f, ringStart, Color.white),
            (2.0f, 200f, ringStart, Color.blue),
            (2.25f, 275f, ringStart, Color.grey),
            (2.5f, 350f, ringStart, Color.yellow),
            (2.75f, 425f, ringStart, Color.red)
        };

        _prefabIcoSpheres = new GameObject[5];

        for (int i = 0; i < 5; i++)
        {
            _prefabIcoSpheres[0] = Resources.Load<GameObject>($"IcoShperes/IcoShpere_{10 ^ i}");
        }

        if (random == null) random = new System.Random();

        GameObjectConversionSettings gameObjectConversionSettings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);

        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // _prefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(Prefab, gameObjectConversionSettings);

        // CreateRings(_ringLayers);
    }
    int GetSizeAndDistanceMultiplier(FieldDepths fieldDepth) => fieldDepth == FieldDepths.Far ? 10 : 1; 
    float GetRingObjectRadialDistance(int ringId, float ringSystemA) => ringSystemA + ringId * Settings.RingWidth * GetSizeAndDistanceMultiplier(Settings.FieldDepth) / (Settings.NumOfRingsAB + 1);
    float GetRingObjectSize(float minSize = 1f, float maxSize = 1000f, Distributions distribution = default) 
    {
        if (distribution == Distributions.White) return (float)(Random.Range(0.0f, 1.0f) * (maxSize - minSize) + minSize);
        if (distribution == Distributions.Normal) // See https://stackoverflow.com/a/218600
        {
            float u1 = 1.0f - Random.Range(0.0f, 1.0f);
            float u2 = 1.0f - Random.Range(0.0f, 1.0f);
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);

            return (maxSize - minSize) / 2 + Settings.StdDeviation * randStdNormal;
        }
        if (distribution == Distributions.HalfNormal) return Mathf.Sqrt(2f) / (Settings.StdDeviation * Mathf.Sqrt(Mathf.PI)) 
            * Mathf.Exp(-Mathf.Pow((float)(Random.Range(0.0f, 1.0f) * (maxSize - minSize) + minSize), 2f) / (2 * Mathf.Pow(Settings.StdDeviation, 2)));

        return 0.0f;
    }
    void CreateRings(List<(float Angle, float YOverhead, float RingStart, Color Color)> ringLayers)
    {
        if (ringLayers == null) return;

        int j = 0;

        foreach (var ringLayer in ringLayers)
        {
            for (int a = 0; a < Settings.RingAngleMaximum; a += Settings.RingAngleStep) 
            {
                for (int i = 0; i < Settings.NumOfRingsAB; i++) 
                {
                    Entity entity = _entityManager.Instantiate(_prefabEntity);

                    AddRingObject(entity, 
                        a + ringLayer.Angle, GetRingObjectRadialDistance(i, ringLayer.RingStart), GetRingObjectSize(Settings.MinRingObjectScale, Settings.MaxRingObjectScale, Distributions.White), 
                        ringLayer.YOverhead, ringLayer.Color, Distributions.White, Settings.MinDeviation, Settings.MaxDeviation, Settings.MinYDeviation, Settings.MaxYDeviation);

                    j++;
                }
            }
        }
    }
    void AddRingObject(Entity entity, float angle, float radius, float scale = 1000f, float yOverhead = 0f, 
        Color color = default, Distributions distribution = default, float minDeviation = -1000f, float maxDeviation = 1000f,
        float minYDeviation = -500f, float maxYDeviation = 500f) 
    {
        float devDiff = maxDeviation - minDeviation;
        float devDiffY = maxYDeviation - minYDeviation;

        if (distribution == Distributions.White)
        {
            angle += (float)(random.NextDouble() * devDiff + minDeviation);
            radius += (float)(random.NextDouble() * devDiff + minDeviation);
            yOverhead += (float)(random.NextDouble() * devDiffY + minYDeviation);
        }

        var position = gameObject.transform.TransformPoint(
            new Vector3(radius * Mathf.Cos(angle) + _coordinateSystemZero.x, yOverhead, radius * Mathf.Sin(angle) + _coordinateSystemZero.z));

        _entityManager.SetComponentData(entity, new Translation { Value = position });
    }
}