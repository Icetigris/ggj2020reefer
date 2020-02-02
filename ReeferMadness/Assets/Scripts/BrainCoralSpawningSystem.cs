using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class BrainCoralSpawningSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        Entities.WithStructuralChanges().ForEach((Entity e, ref BrainCoralSpawner spawner, in LocalToWorld location) =>
        {
            using (var corals = EntityManager.Instantiate(spawner.prefab, spawner.spawnCount, Allocator.Temp))
            {
                for (int i = 0; i < corals.Length; ++i)
                {
                    EntityManager.SetComponentData(corals[i], new Translation
                    {
                        Value = spawner.spawnPosition
                    });
                }
            }
            EntityManager.DestroyEntity(spawner.prefab);
            EntityManager.DestroyEntity(e);
        }).Run();

        return inputDependencies;
    }
}
