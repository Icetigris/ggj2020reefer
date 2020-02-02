using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using static Unity.Mathematics.math;

public class MouseInteractionSystem : JobComponentSystem
{
    [BurstCompile]
    struct MouseInteractionSystemJob : IJobForEach<Translation, Rotation>
    {
        public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation)
        {
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new MouseInteractionSystemJob();


        return job.Schedule(this, inputDependencies);
    }
}