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
        public void Execute(ref Coral coral, ref NonUniformScale scale)
        {
            // scale coral up by scale vector * scale factor * deltaTime * hp-based growth rate
            scale.Value = scale.Value + coral.growthScaleFactor * jobDeltaTime * (coral.HP / 100f);
            //localToWorld = new LocalToWorld
            //{
            //    Value = float4x4.TRS(localToWorld.Position.xyz,
            //                    localToWorld.Rotation, new float3(1.0f, 1.0f, 1.0f))
            //};
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new SimulateEnvironmentJob()
        {
            jobDeltaTime = Time.DeltaTime
        };
        var simulateEnvironmentJobHandle = job.Schedule(this, inputDependencies);

        var simulateCoralGrowthJob = new SimulateCoralGrowth()
        {
            jobDeltaTime = Time.DeltaTime
        };
        var simulateCoralGrowthJobHandle = simulateCoralGrowthJob.Schedule(this, simulateEnvironmentJobHandle);

        return simulateCoralGrowthJobHandle;
    }
}