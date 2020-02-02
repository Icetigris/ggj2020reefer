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
    public float minTemp;
    public float maxTemp;
    public float minPH;
    public float maxPH;
    public float reactionRate;
    //public float totalBiomass;
}

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class SeaSimulationStateAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Temperature = 83;
    public float pH = 7.5f;
    public float MinTemperature = 75;
    public float MaxTemperature = 85;
    public float MinPH = 7;
    public float MaxPH = 8.4f;
    public float ReactionRate = 0.1f;
    //public float TotalBiomass;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new SeaSimulationState
        {
            temperature = Temperature,
            pH = pH,
            minTemp = MinTemperature,
            maxTemp = MaxTemperature,
            minPH = MinPH,
            maxPH = MaxPH,
            reactionRate = ReactionRate
        });
    }
}