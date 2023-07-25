using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class RoverDeployer
{
    public Config Configuration;
    public MarsRover Rover;
    private readonly ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();
    private readonly IMapLoader _mapLoader = new MapLoader.MapLoader();
    private Map _map;
    

    public RoverDeployer(Config configuration)
    {
        Configuration = configuration;
        Rover = CreateAndDeployMarsRover();
    }

    private MarsRover CreateAndDeployMarsRover()
    {
        _map = _mapLoader.Load(Configuration.filepath);
        var possibleStartingPointOfRover = _coordinateCalculator.GetAdjacentCoordinates(Configuration.landingPoint, _map.Representation.GetLength(0))
            .Where(c => _map.Representation[c.X, c.Y] == " ");
        Random random = new Random();
        Coordinate startingPointOfRover =
            possibleStartingPointOfRover.ToList()[random.Next(possibleStartingPointOfRover.Count())];
        return new MarsRover(startingPointOfRover, new Dictionary<string, List<Coordinate>>());
    }
    
}