using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public static class FindEmptyAdjacentField
{
    public static IEnumerable<Coordinate> FindEmptyAdjacentFields(ICoordinateCalculator coordinateCalculator, Map map, Coordinate currentPosition, Coordinate landingSpot)
    {
        var toReturn = coordinateCalculator.GetAdjacentCoordinates(currentPosition, map.Representation.GetLength(0))
                .Where(c => (map.Representation[c.X, c.Y] == " " || map.Representation[c.X, c.Y] == "A") && c != landingSpot);
        return toReturn;
    }
    
}