using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public struct Coral : IComponentData
{
    public float3 growthScaleFactor;
    public float maxScale;
    public float HP;
    public float3 spawnOffset;
    public float spawnRadius;
}

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class CoralAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float3 GrowthScaling = new float3(1, 1, 1);
    public float MaxScale = 10f;
    public float baseHP = 20f;
    public float3 SpawnOffset = new float3(0,1,0);
    public float SpawnRadius = 10;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData<Coral>(entity, new Coral
        {
            growthScaleFactor = GrowthScaling,
            maxScale = MaxScale,
            HP = baseHP,
            spawnOffset = SpawnOffset,
            spawnRadius = SpawnRadius
        });

        dstManager.AddComponentData<NonUniformScale>(entity, new NonUniformScale
        {
            Value = new float3(1, 1, 1)
        });
    }
}
