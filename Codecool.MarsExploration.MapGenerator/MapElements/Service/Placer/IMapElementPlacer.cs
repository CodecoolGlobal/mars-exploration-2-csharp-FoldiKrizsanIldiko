using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapGenerator.MapElements.Service.Placer;

public interface IMapElementPlacer
{
    bool CanPlaceElement(MapElement element,ref string?[,] map, Coordinate coordinate);
    void PlaceElement(MapElement element,ref string?[,] map, Coordinate coordinate);
}
