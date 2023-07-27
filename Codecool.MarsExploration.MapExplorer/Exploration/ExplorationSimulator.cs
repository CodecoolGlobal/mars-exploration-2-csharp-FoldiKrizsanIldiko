using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.SimulationSteps;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.Output.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public class ExplorationSimulator
{
    public static Config _config;
    public RoverDeployer _roverDeployer;
    public static SimulationContext _simulationContext;
    private static ILogger _cLogger= new ConsoleLogger();
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    private static ILogger _fLogger = new FileLogger($@"{WorkDir}\Output\log.txt");
    private  ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();

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

        Movement.BackToTheShip(_simulationContext);
        IMapFileWriter fw = new MapFileWriter();
        fw.WriteMapFile(_simulationContext.Map,$@"{WorkDir}\Output\mappp.map" );
    }
    

    public static void LoggingTheStep()
    {
        _cLogger.Log($" {_simulationContext.MarsRover} {_simulationContext.NumberOfSteps} step");
        _fLogger.Log($" {_simulationContext.MarsRover} {_simulationContext.NumberOfSteps} step");
    }

    public static void LoggingTheStep(string typeofEvent)
    {
        _cLogger.Log($" {typeofEvent} {_simulationContext.MarsRover} ");
        _fLogger.Log($" {typeofEvent} {_simulationContext.MarsRover} ");
    }

    public void StepIncrement()
    {
        _simulationContext.NumberOfSteps++;
    }
}