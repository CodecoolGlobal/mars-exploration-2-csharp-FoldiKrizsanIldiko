using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.SimulationSteps;

public static class ScanResources
{
    public static Dictionary<string, HashSet<Coordinate>> ScanResource(ICoordinateCalculator coordinateCalculator,
        Map map, Coordinate currentPosition, MarsRover.MarsRover rover)
    {
        var toReturn = coordinateCalculator.GetAdjacentCoordinates(currentPosition, map.Representation.GetLength(0), rover.SightDistance);
        foreach (var c in toReturn)
        {
            if (rover.EncounteredResources.ContainsKey(map.Representation[c.X, c.Y]))
            { 
                rover.EncounteredResources[map.Representation[c.X, c.Y]].Add(c);
            }
        }

        return rover.EncounteredResources;
    }
}