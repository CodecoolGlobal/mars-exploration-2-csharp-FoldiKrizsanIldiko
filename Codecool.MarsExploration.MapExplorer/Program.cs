using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Configuration;

namespace Codecool.MarsExploration.MapExplorer;

class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        string mapFile = $@"{WorkDir}\Resources\exploration-0.map";
        Coordinate landingSpot = new Coordinate(3, 3);
        IEnumerable<string> resources = new List<string>()
        {
            "#",//mountain"🏔️",
            "&",//pit"🕳️"
            "*",//mineral💎
            "%"//water🌊
        };
        int stepNumber = 300;

        Config firstConfiguration = new Config(mapFile, landingSpot, resources, stepNumber);
        
        // IMapLoader mapLoader = new MapLoader.MapLoader();
        // mapLoader.Load(mapFile);

        
        IConfigValidator configValidator = new ConfigValidator(firstConfiguration);
        configValidator.IsConfigValid();

        RoverDeployer roverDeployer = new RoverDeployer(firstConfiguration);
        Console.WriteLine(roverDeployer.Rover);
    }
}
