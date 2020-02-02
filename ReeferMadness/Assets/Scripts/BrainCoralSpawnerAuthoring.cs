using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public struct BrainCoralSpawner : IComponentData
{
    public Entity prefab;
    public float3 spawnPosition;
    public int spawnCount;
}

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class BrainCoralSpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject coralPrefab;
    public float3 spawnPosition;
    public int coralsToSpawn;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new BrainCoralSpawner
        {
            prefab = conversionSystem.GetPrimaryEntity(coralPrefab),
            spawnPosition = spawnPosition,
            spawnCount = coralsToSpawn
        });
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(coralPrefab);
    }
}
