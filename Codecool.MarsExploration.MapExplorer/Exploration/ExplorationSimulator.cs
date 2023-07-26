using System.Runtime.InteropServices.ComTypes;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MarsRover;

namespace Codecool.MarsExploration.MapExplorer.Exploration;
// Everything is in place to start working on the simulation engine.
// The simulator must receive a configuration object, contact the necessary services,
// and use the necessary methods to run the simulation.
// You need to make some decisions about the implementation details of the simulator class. We recommend the following:
//
// Generate the context first, then simulate the rover exploration runfter. (SLAP)
//
// Generating the context implies loading the chart (reading the file), validating the landing coordinates
// for the spaceship and deploying the rover in an empty coordinate adjacent to the spaceship.
// Each one of these responsibilities can be in different classes (SLAP & SRP)
//
// The simulation of the rover exploration run can be arranged as a loop that repeats until an outcome is found.
// Each iteration of the loop can run a series of ordered simulation steps. (SRP & OCP)

public class ExplorationSimulator
{
    private static Config _config;
    private static RoverDeployer _roverDeployer;
    private static SimulationContext _simulationContext;

    public ExplorationSimulator(Config config)
    {
        _config = config;
        _roverDeployer = new RoverDeployer(_config);
        _simulationContext = new SimulationContext(0, _roverDeployer.Rover, _config);
    }
   
    
        
    public void CreateSimulationContext()
    {
        Console.WriteLine(_simulationContext.Outcome == ExplorationOutcome.Error);
        // while (_simulationContext.Outcome == null)
        // {
        //     Console.WriteLine("create sim context running");
        //     // SimulationSteps addig fut, amíg nem lesz az outcome-nak értéke
        //     //
        // }
    }
    
}