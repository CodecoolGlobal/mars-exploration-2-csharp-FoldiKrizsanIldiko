using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration;


//The simulation context contains the elements required to run the simulation. It is a data object used by the simulator engine.
//It must contain at least the following information:

//     The number of steps.
//
//     The number of steps required to reach a timeout.
//
//     The rover.
//
//     The location of the spaceship. -- CONFIG
//
//     The map. -- CONFIG
//
//     The symbols of resources to monitor. -- CONFING
//
//     The exploration outcome.

public class SimulationContext
{
    public int NumberOfSteps { get; set; }
    public int TimeOutSteps { get; }
    public MarsRover.MarsRover MarsRover { get; }
    public Coordinate LocationOfShip { get; }
    public Map Map { get; }
    public IEnumerable<string> ResourcesToFind {get;}
    public ExplorationOutcome Outcome { get; set; }

    private MapLoader.MapLoader _mapLoader = new MapLoader.MapLoader();

    public SimulationContext(int numberOfSteps, int timeOutSteps, MarsRover.MarsRover marsRover, Config config)
    {
        NumberOfSteps = numberOfSteps;
        TimeOutSteps = timeOutSteps;
        MarsRover = marsRover;
        LocationOfShip = config.landingPoint;
        Map = _mapLoader.Load(config.filepath);
        ResourcesToFind = config.resources;
    }
}