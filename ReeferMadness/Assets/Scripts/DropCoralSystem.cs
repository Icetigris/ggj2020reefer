using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

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

        if(Input.GetButtonUp("Fire1"))
        {
            Entities.WithStructuralChanges().ForEach((Entity e, ref BrainCoralSpawner spawner) =>
            {
                using (var corals = EntityManager.Instantiate(spawner.prefab, spawner.spawnCount, Allocator.Temp))
                {
                    for (int i = 0; i < corals.Length; ++i)
                    {
                        EntityManager.SetComponentData(corals[i], new Translation
                        {
                            Value = playerPosition.position
                        });
                    }
                }
            }).Run();
        }

        return inputDependencies;
    }
}