using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public struct BrainCoral : IComponentData
{
    public float growthRate;
}

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class BrainCoralAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float growthRate;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new BrainCoral
        {
            growthRate = growthRate
        });
    }
}
