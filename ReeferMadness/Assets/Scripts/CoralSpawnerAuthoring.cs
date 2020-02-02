using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class CoralSpawnerAuthoring : MonoBehaviour
{
    public List<GameObject> coralPrefabs;
    public static List<Entity> convertedPrefabs;
    public static string[] prefabNames = { "Brain", "Branch", "Shelf" };
    void Start()
    {
        // Create entity prefab from the game object hierarchy once
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        convertedPrefabs = new List<Entity>(coralPrefabs.Count);

        for(int i = 0; i < coralPrefabs.Count; i++)
        {
            convertedPrefabs.Add(GameObjectConversionUtility.ConvertGameObjectHierarchy(coralPrefabs[i], settings));
        }
    }
}