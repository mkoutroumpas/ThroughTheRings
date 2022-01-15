using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using System.Collections.Generic;

public class RingSystem_EntitySpawner : MonoBehaviour
{
    #region Private variables
    GameObject[] _prefabIcoSpheres;
    Entity[] _prefabEntities;
    EntityManager _entityManager;
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

        GameObjectConversionSettings gameObjectConversionSettings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);

        _prefabIcoSpheres = new GameObject[9];
        _prefabEntities = new Entity[9];

        _prefabIcoSpheres[8] = Resources.Load("IcoShpere_10", typeof(GameObject)) as GameObject;
        _prefabIcoSpheres[7] = Resources.Load("IcoShpere_20", typeof(GameObject)) as GameObject;
        _prefabIcoSpheres[6] = Resources.Load("IcoShpere_50", typeof(GameObject)) as GameObject;
        _prefabIcoSpheres[5] = Resources.Load("IcoShpere_100", typeof(GameObject)) as GameObject;
        _prefabIcoSpheres[4] = Resources.Load("IcoShpere_200", typeof(GameObject)) as GameObject;
        _prefabIcoSpheres[3] = Resources.Load("IcoShpere_500", typeof(GameObject)) as GameObject;
        _prefabIcoSpheres[2] = Resources.Load("IcoShpere_1000", typeof(GameObject)) as GameObject;
        _prefabIcoSpheres[1] = Resources.Load("IcoShpere_2000", typeof(GameObject)) as GameObject;
        _prefabIcoSpheres[0] = Resources.Load("IcoShpere_5000", typeof(GameObject)) as GameObject;

        for (int i = 0; i < 9; i++)
        {
            _prefabEntities[i] = GameObjectConversionUtility.ConvertGameObjectHierarchy(_prefabIcoSpheres[i], gameObjectConversionSettings);
        }

        if (random == null) random = new System.Random();

        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        CreateRings(_ringLayers);
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

        int totalEntityCount = ringLayers.Count * (int)(Settings.RingAngleMaximum / Settings.RingAngleStep) * Settings.NumOfRingsAB;

        int entityIndex = -1;
        int[] entityIndexes = new int[9];

        foreach (var ringLayer in ringLayers)
        {
            for (int a = 0; a < Settings.RingAngleMaximum; a += Settings.RingAngleStep) 
            {
                for (int i = 0; i < Settings.NumOfRingsAB; i++) 
                {
                    Entity entity = GetNewEntity(_entityManager, j, totalEntityCount, out entityIndex);

                    entityIndexes[entityIndex]++;

                    AddRingObject(entity, 
                        a + ringLayer.Angle, GetRingObjectRadialDistance(i, ringLayer.RingStart), GetRingObjectSize(Settings.MinRingObjectScale, Settings.MaxRingObjectScale, Distributions.White), 
                        ringLayer.YOverhead, ringLayer.Color, Distributions.White, Settings.MinDeviation, Settings.MaxDeviation, Settings.MinYDeviation, Settings.MaxYDeviation);

                    j++;
                }
            }
        }

        Debug.Log($"Total j = {j}");

        for (int i = 0; i < entityIndexes.Length; i++)
        {
            Debug.Log($"Total for category {i}: {entityIndexes[i]}");
        }
    }
    Entity GetNewEntity(EntityManager entityManager, int index, int totalEntityCount, out int entityIndex)
    {
        float v = (float)index / (float)totalEntityCount;

        entityIndex = -1;

        if (v <= 1.0f && v > 0.9f) entityIndex = 0;
        if (v <= 0.9f && v > 0.8f) entityIndex = 1;
        if (v <= 0.8f && v > 0.7f) entityIndex = 2;
        if (v <= 0.7f && v > 0.6f) entityIndex = 3;
        if (v <= 0.6f && v > 0.5f) entityIndex = 4;
        if (v <= 0.5f && v > 0.4f) entityIndex = 5;
        if (v <= 0.4f && v > 0.3f) entityIndex = 6;
        if (v <= 0.3f && v > 0.2f) entityIndex = 7;
        if (v <= 0.2f) entityIndex = 8;

        return _entityManager.Instantiate(_prefabEntities[entityIndex]);
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