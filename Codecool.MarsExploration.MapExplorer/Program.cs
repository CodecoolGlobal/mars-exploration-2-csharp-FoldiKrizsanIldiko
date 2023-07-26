using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Configuration;
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
        explorationSimulator.CreateSimulationContext();
        explorationSimulator.LoggingTheStep();
        for (int i = 0; i < 20; i++)
        {
            explorationSimulator.Movement();
        }

        IMapFileWriter fw = new MapFileWriter();
        fw.WriteMapFile(explorationSimulator._simulationContext.Map,$@"{WorkDir}\Output\mappp.map" );

    }
}
