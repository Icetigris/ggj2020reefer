using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct SeaSimulationState : IComponentData
{
    public float temperature;
    public float pH;
}

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class SeaSimulationStateAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Temperature = 78;
    public float pH = 8.4f;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new SeaSimulationState
        {
            temperature = Temperature,
            pH = pH
        });
    }
}