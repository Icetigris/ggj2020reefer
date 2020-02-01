using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class CoralGrowthSystem : JobComponentSystem
{
    // Brain coral uniformly scales up until it gets to a maximum size, then it spawns a new one
    [BurstCompile]
    struct BrainCoralGrowthJob : IJobForEach<Translation, Rotation>
    {
        public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation)
        {
        }
    }

    // Shelf coral scales along 2 axes
    // hard mode: make the boys wavy

    // Branching coral scales up branches and then makes new segments
    // easy mode: just make some new segments

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        return inputDependencies;
    }
}