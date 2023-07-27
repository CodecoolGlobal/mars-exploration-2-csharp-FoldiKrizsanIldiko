using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.SimulationSteps;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.Configuration;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using Codecool.MarsExploration.MapGenerator.Output.Service;

namespace Codecool.MarsExploration.MapExplorer;

class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        ILogger cLogger = new ConsoleLogger();
        ILogger fLogger = new FileLogger($@"{WorkDir}\Output\log.txt");
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

        ExplorationSimulator explorationSimulator = new ExplorationSimulator(firstConfiguration);
        
        
        explorationSimulator.RunSimulation();
        
       //  ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
       //  IMapLoader mapLoader = new MapLoader.MapLoader();
       //  Map map = mapLoader.Load(mapFile);
       //  IEnumerable<Coordinate> testingEmptyFields = FindEmptyAdjacentField.FindEmptyAdjacentFields(coordinateCalculator, map, new Coordinate(5, 11), landingSpot);
       // testingEmptyFields.ToList().ForEach(e=>Console.Write(e));

    }
}
