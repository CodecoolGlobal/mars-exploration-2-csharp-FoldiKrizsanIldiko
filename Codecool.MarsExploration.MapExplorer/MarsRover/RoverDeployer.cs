using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class RoverDeployer
{
    private readonly Config _configuration;
    public readonly MarsRover Rover;
    private readonly ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();
    private readonly IMapLoader _mapLoader = new MapLoader.MapLoader();
    private Map? _map;
    
    public RoverDeployer(Config configuration)
    {
        _configuration = configuration;
        Rover = CreateAndDeployMarsRover();
    }

    private MarsRover CreateAndDeployMarsRover()
    {
        _map = _mapLoader.Load(_configuration.filepath);
        IConfigValidator configValidator = new ConfigValidator(_configuration);
        if (!configValidator.IsConfigValid(_map))
        {
            throw new Exception("Invalid config exception.");
        }

        var possibleStartingPointOfRover = _coordinateCalculator.GetAdjacentCoordinates(_configuration.landingPoint, _map.Representation.GetLength(0))
            .Where(c => _map.Representation[c.X, c.Y] == " ");
        
        Random random = new Random();
        Coordinate startingPointOfRover =
            possibleStartingPointOfRover.ToList()[random.Next(possibleStartingPointOfRover.Count())];
        
        return new MarsRover(startingPointOfRover);
    }
}