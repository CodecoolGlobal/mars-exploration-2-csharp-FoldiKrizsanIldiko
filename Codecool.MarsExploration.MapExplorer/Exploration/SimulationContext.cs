using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public class SimulationContext
{
    public int NumberOfSteps { get; set; }
    public int TimeOutSteps { get; }
    public MarsRover.MarsRover MarsRover { get; }
    public Coordinate LocationOfShip { get; }
    public Map Map { get; }
    public ExplorationOutcome Outcome { get; set; }

    private readonly MapLoader.MapLoader _mapLoader = new ();

    public SimulationContext(int numberOfSteps, MarsRover.MarsRover marsRover, Config config)
    {
        NumberOfSteps = numberOfSteps;
        TimeOutSteps = config.steps;
        MarsRover = marsRover;
        LocationOfShip = config.landingPoint;
        Map = _mapLoader.Load(config.filepath);
    }
}