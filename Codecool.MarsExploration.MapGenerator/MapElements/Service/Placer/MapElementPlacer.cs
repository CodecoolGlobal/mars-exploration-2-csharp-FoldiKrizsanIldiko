using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapGenerator.MapElements.Service.Placer;

public class MapElementPlacer : IMapElementPlacer
{
    public bool CanPlaceElement(MapElement element, ref string?[,] map, Coordinate coordinate)
    {
        return element.Dimension == 1
            ? CanPlaceOneDimensionalElement(map, coordinate)
            : CanPlaceMultiDimensionalElement(element, ref map, coordinate);
    }

    private static bool CanPlaceOneDimensionalElement(string?[,] map, Coordinate coordinate)
    {
        return coordinate.X >= 0
               && coordinate.X < map.GetLength(0)
               && coordinate.Y >= 0
               && coordinate.Y < map.GetLength(1)
               && (map[coordinate.X, coordinate.Y] == null||map[coordinate.X, coordinate.Y] =="|"||map[coordinate.X, coordinate.Y] == "/");
    }

    private static bool CanPlaceMultiDimensionalElement(MapElement element, ref string?[,] map, Coordinate coordinate)
    {
        if (coordinate.X + element.Dimension >= map.GetLength(0) || coordinate.X < 0)
        {
            return false;
        }

        if (coordinate.Y + element.Dimension >= map.GetLength(1) || coordinate.Y < 0)
        {
            return false;
        }

        List<Coordinate> coordinateListForSquare = new List<Coordinate>();

        for (int i = coordinate.X; i < element.Dimension + coordinate.X; i++)
        {
            for (int j = coordinate.Y; j < element.Dimension + coordinate.Y; j++)
            {
                coordinateListForSquare.Add(new Coordinate(i, j));
            }
        }

        foreach (var c in coordinateListForSquare)
        {
            if (map[c.X, c.Y] != null)
            {
                return false;
            }
        }

        return true;
    }

    public void PlaceElement(MapElement element, ref string?[,] map, Coordinate coordinate)
    {
        for (int i = 0; i < element.Representation.GetLength(0); i++)
        {
            for (int j = 0; j < element.Representation.GetLength(1); j++)
            {
                map[coordinate.X + i, coordinate.Y + j] = element.Representation[i, j];
            }
        }
    }
}