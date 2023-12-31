﻿using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.Configuration.Model;
using Codecool.MarsExploration.MapGenerator.Configuration.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Service.Placer;

namespace Codecool.MarsExploration.MapGenerator.MapElements.Service.Generator;

public class MapGenerator : IMapGenerator
{
    private static readonly Random Random = new();
    private static Map EmptyMap => new(new string[,] { }, false);

    private readonly IMapConfigurationValidator _mapConfigValidator;
    private readonly IMapElementsGenerator _mapElementsGenerator;
    private readonly ICoordinateCalculator _coordinateCalculator;
    private readonly IDimensionCalculator _dimensionCalculator;
    private readonly IMapElementPlacer _mapElementPlacer;

    public MapGenerator(
        IMapConfigurationValidator mapConfigValidator,
        IMapElementsGenerator mapElementsGenerator,
        ICoordinateCalculator coordinateCalculator,
        IDimensionCalculator dimensionCalculator,
        IMapElementPlacer mapElementPlacer)
    {
        _mapConfigValidator = mapConfigValidator;
        _mapElementsGenerator = mapElementsGenerator;
        _coordinateCalculator = coordinateCalculator;
        _dimensionCalculator = dimensionCalculator;
        _mapElementPlacer = mapElementPlacer;
    }

    public Map Generate(MapConfiguration mapConfig)
    {
        if (!_mapConfigValidator.Validate(mapConfig))
        {
           
            return EmptyMap;
        }

        var dimension = _dimensionCalculator.CalculateDimension(mapConfig.MapSize, 0);
        string?[,] map = new string?[dimension, dimension];

        var elements =
            new Stack<MapElement>(_mapElementsGenerator.CreateAll(mapConfig).OrderBy(e => e.Dimension));//make ascending first is smallest [1,1,8,8...]

        while (elements.Any())
        {
            var element = elements.Pop(); // get LAST element, alias greatest
            var coord = GetTargetCoordinate(element, map);
            if (_mapElementPlacer.CanPlaceElement(element,ref map, coord))
            {
                _mapElementPlacer.PlaceElement(element,ref map, coord);
            }
            else
            {
                elements.Push(element);
            }
        }
        int rows = map.GetLength(0);
        int columns = map.GetLength(1);
                
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (map[i, j] == null)
                {
                    map[i, j] = " ";
                } 
                Console.Write(map[i, j]); // Adjust the spacing as needed
            }
            Console.WriteLine();
        }

        return new Map(map, true);
    }

    private Coordinate GetTargetCoordinate(MapElement element, string?[,] map)
    {
        if (element.PreferredLocationSymbol != null)
        {
            var preferredLocations = GetPreferredLocations(element.PreferredLocationSymbol, map);
            var emptyAdjacent = GetEmptyAdjacent(preferredLocations, map).ToList();

            return emptyAdjacent.Any()
                ? emptyAdjacent[Random.Next(emptyAdjacent.Count)]
                : _coordinateCalculator.GetRandomCoordinate(map.GetLength(0));
        }

        return _coordinateCalculator.GetRandomCoordinate(map.GetLength(0));
    }

    private static IEnumerable<Coordinate> GetPreferredLocations(string preferredLocationSymbol, string?[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == preferredLocationSymbol)
                {
                    yield return new Coordinate(i, j);
                }
            }
        }
    }

    private IEnumerable<Coordinate> GetEmptyAdjacent(IEnumerable<Coordinate> coordinates, string?[,] map)
    {
        var toRetun = _coordinateCalculator.GetAdjacentCoordinates(coordinates, map.GetLength(0))//nálunk GetAdjeForMany
                                  .Where(c => map[c.X, c.Y] == "null" || map[c.X, c.Y] == "/" || map[c.X, c.Y] == "|" );
        
        return toRetun;
    }
}
