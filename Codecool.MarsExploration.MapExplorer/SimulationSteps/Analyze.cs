using System.Runtime.InteropServices;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.SimulationSteps;

public static class Analyze
{
    private static Dictionary<string, uint> outcomeCondition = new()
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
            allConditionResults.Add(outcomeCondition.ContainsKey(resource.Key) && resource.Value.Count() >= outcomeCondition[resource.Key]);
        }

        if (allConditionResults.All(res => res))
        {
            return ExplorationOutcome.Colonizable;
        }

        return ExplorationOutcome.Error;
    }
}