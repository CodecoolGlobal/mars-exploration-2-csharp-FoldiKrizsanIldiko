using System.Runtime.InteropServices;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.SimulationSteps;

public static class Analyze
{
    private static readonly Dictionary<string, uint> OutcomeCondition = new()
    {
        {"#", 1},
        {"&", 1},
        {"*", 1},
        {"%", 1}
    };

    public static ExplorationOutcome CheckOutcome(MarsRover.MarsRover rover)
    {
        List<bool> allConditionResults = new List<bool>();
        foreach (var resource in rover.EncounteredResources)
        {
            allConditionResults.Add(OutcomeCondition.ContainsKey(resource.Key) && resource.Value.Count() >= OutcomeCondition[resource.Key]);
        }

        if (allConditionResults.All(res => res))
        {
            return ExplorationOutcome.Colonizable;
        }

        return ExplorationOutcome.Error;
    }
}