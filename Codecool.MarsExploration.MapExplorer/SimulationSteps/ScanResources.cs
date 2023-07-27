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
        var fieldsToWatchFrom = new HashSet<Coordinate>
        {
            new (simulationContext.MarsRover.CurrentPosition.X,
                simulationContext.MarsRover.CurrentPosition.Y)
        };
        
        for (int i = 0; i < simulationContext.MarsRover.SightDistance; i++)
        {
            var fieldsTWF = new HashSet<Coordinate>();

            foreach (var coordinate in fieldsToWatchFrom.ToArray())
            {
                var toReturn = coordinateCalculator.GetAdjacentCoordinates(coordinate, simulationContext.Map.Representation.GetLength(0));
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
                    fieldsTWF.Add(c);
                }
            }
            fieldsToWatchFrom.UnionWith(fieldsTWF);
        }
    }
}