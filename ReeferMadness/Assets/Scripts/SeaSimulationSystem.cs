using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class SeaSimulationSystem : JobComponentSystem
{
    [BurstCompile]
    struct SimulateEnvironmentJob : IJobForEach<SeaSimulationState>
    {
        public float jobDeltaTime;
        public void Execute(ref SeaSimulationState simState)
        {
            var newTemp = simState.temperature - (/*(kelpBiomassTotal/2 + totalBiomass) **/ simState.reactionRate * jobDeltaTime);
            simState.temperature = clamp(newTemp, simState.minTemp, simState.maxTemp);

            var newpH = simState.pH + (/*(kelpBiomassTotal/2 + totalBiomass) **/ simState.reactionRate * jobDeltaTime);
            simState.pH = clamp(newpH, simState.minPH, simState.maxPH);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new SimulateEnvironmentJob()
        {
            jobDeltaTime = Time.DeltaTime
        };
        return job.Schedule(this, inputDependencies);
    }
}