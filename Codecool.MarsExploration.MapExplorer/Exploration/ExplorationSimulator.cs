using System.Runtime.InteropServices.ComTypes;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.SimulationSteps;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using Codecool.MarsExploration.MapGenerator.Output.Service;

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
    public static Config _config;
    public RoverDeployer _roverDeployer;
    public static SimulationContext _simulationContext;
    private static ILogger _cLogger= new ConsoleLogger();
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    private static ILogger _fLogger = new FileLogger($@"{WorkDir}\Output\log.txt");// constructoron kívül példányosítani
    private  ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();
    private  Random _random = new Random();

    public ExplorationSimulator(Config config)
    {
        _config = config;
        _roverDeployer = new RoverDeployer(_config);
        _simulationContext = new SimulationContext(1,_roverDeployer.Rover, _config);
    }


    public void RunSimulation()
    {   
        for (int i = 1; i <= _simulationContext.TimeOutSteps; i++)
        {
            Movement.MoveTheRover(_simulationContext);
            StepIncrement();
            _simulationContext.MarsRover.EncounteredResources = ScanResources.ScanResource(_coordinateCalculator, _simulationContext.Map,
                _simulationContext.MarsRover.CurrentPosition, _simulationContext.MarsRover);
            
            ExplorationOutcome outcome = Analyze.CheckOutcome(_simulationContext.MarsRover);
            
            if (outcome != ExplorationOutcome.Error)
            {
                _simulationContext.Outcome = outcome;
                LoggingTheStep($"{outcome}");
                break;
            }

            if (i == _simulationContext.TimeOutSteps)
            {
                _simulationContext.Outcome = ExplorationOutcome.Timeout;
                LoggingTheStep($"{_simulationContext.Outcome}");
            }
        }

        

        foreach (var resource in _simulationContext.MarsRover.EncounteredResources)
        {
            Console.WriteLine(resource.Key);
            foreach (var value in resource.Value)
            {
                Console.WriteLine(value);
            }
        }
        Movement.BackToTheShip(_simulationContext);
        IMapFileWriter fw = new MapFileWriter();
        fw.WriteMapFile(_simulationContext.Map,$@"{WorkDir}\Output\mappp.map" );
    }
    

    public static void LoggingTheStep() // írja hol tart
    {
        _cLogger.Log($" {_simulationContext.MarsRover} {_simulationContext.NumberOfSteps} step");
        _fLogger.Log($" {_simulationContext.MarsRover} {_simulationContext.NumberOfSteps} step");
    }

    public static void LoggingTheStep(string typeofEvent)// ha eredmény van akkor írja az eredményt
    {
        _cLogger.Log($" {typeofEvent} {_simulationContext.MarsRover} ");
        _fLogger.Log($" {typeofEvent} {_simulationContext.MarsRover} ");
    }

    public void StepIncrement()
    {
        _simulationContext.NumberOfSteps++;
    }
}