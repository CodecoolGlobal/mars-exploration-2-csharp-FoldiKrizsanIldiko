using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.SimulationSteps;

public static class ScanResources
{
    public static void ScanResource(ICoordinateCalculator coordinateCalculator,
        SimulationContext simulationContext)
    {
        var toReturn = coordinateCalculator.GetAdjacentCoordinates(simulationContext.MarsRover.CurrentPosition, simulationContext.Map.Representation.GetLength(0), simulationContext.MarsRover.SightDistance);
        foreach (var c in toReturn)
        {
            if (simulationContext.Map.Representation[c.X, c.Y] == " ")
            {
                simulationContext.Map.Representation[c.X, c.Y] = "C";
            }

            if (simulationContext.MarsRover.EncounteredResources.ContainsKey(simulationContext.Map.Representation[c.X, c.Y]))
            {
                simulationContext.MarsRover.EncounteredResources[simulationContext.Map.Representation[c.X, c.Y]].Add(c);
            }
        }
    }
}