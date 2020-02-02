using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// This is the current species you have in your hand (or butt) to plant.
public struct EquippedCoral : IComponentData
{
    public CoralType equippedType;
}

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class EquippedSeedAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public CoralType equipped;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData<EquippedCoral>(entity, new EquippedCoral { equippedType = equipped });
        dstManager.RemoveComponent<Translation>(entity);
        dstManager.RemoveComponent<Rotation>(entity);
    }
}
