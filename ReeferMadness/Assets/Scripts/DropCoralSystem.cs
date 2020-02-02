using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public enum CoralType
{
    Brain = 0,
    Branching,
    Shelf,
    Cone,
    Tube,
    TypeCount
}

public class DropCoralSystem : JobComponentSystem
{
    private Transform playerPosition;

    // poop out some coral
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        if(playerPosition == null)
        {
            playerPosition = GameObject.Find("CharacterController").transform;
        }

        //handle weapon change
        if (Input.GetButtonDown("Fire2"))
        {
            var currentlyEquippedSeed = GetSingleton<EquippedCoral>();
            var currentlyEquippedSeedEntity = GetSingletonEntity<EquippedCoral>();
            var newtype = (CoralType)((int)(currentlyEquippedSeed.equippedType + 1) % (int)CoralType.TypeCount);
            EntityManager.SetComponentData<EquippedCoral>(currentlyEquippedSeedEntity, new EquippedCoral { equippedType = newtype });
        }

        if (Input.GetButtonUp("Fire1"))
        {
            var currentlyEquippedSeed = GetSingleton<EquippedCoral>();
            var coral = EntityManager.Instantiate(CoralSpawnerAuthoring.convertedPrefabs[(int)currentlyEquippedSeed.equippedType]);
            EntityManager.SetComponentData(coral, new Translation
            {
                Value = playerPosition.position
            });
            EntityManager.SetName(coral, CoralSpawnerAuthoring.prefabNames[(int)currentlyEquippedSeed.equippedType]);
        }

        return inputDependencies;
    }
}