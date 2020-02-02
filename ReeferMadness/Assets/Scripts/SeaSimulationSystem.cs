using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

[UpdateInGroup(typeof(SimulationSystemGroup))]
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

    [BurstCompile]
    struct SimulateCoralGrowth : IJobForEach<Coral, NonUniformScale>
    {
        public float jobDeltaTime;
        [ReadOnly] public SeaSimulationState simState;
        public void Execute(ref Coral coral, ref NonUniformScale scale)
        {
            scale.Value = clamp(scale.Value + coral.growthScaleFactor * jobDeltaTime * (coral.HP / 100f), new float3(0.1f,0.1f,0.1f), coral.growthScaleFactor * coral.maxScale);

            //Health =  fullReefHealth - ( (delta ideal pH and current pH)+(delta ideal temp and current Temp)) * reductionFactor * time
            coral.HP = clamp(coral.HP - ((simState.idealPh - simState.pH) + (simState.idealTemp - simState.temperature)) /** simState.reactionRate*/ * jobDeltaTime, 0, 100);
        }
    }

    // more coral raises the ph
    // more kelp lowers temp

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new SimulateEnvironmentJob()
        {
            jobDeltaTime = Time.DeltaTime
        };
        var simulateEnvironmentJobHandle = job.Schedule(this, inputDependencies);

        var simulateCoralGrowthJob = new SimulateCoralGrowth()
        {
            jobDeltaTime = Time.DeltaTime,
            simState = GetSingleton<SeaSimulationState>()
        };
        var simulateCoralGrowthJobHandle = simulateCoralGrowthJob.Schedule(this, simulateEnvironmentJobHandle);

        return simulateCoralGrowthJobHandle;
    }
}