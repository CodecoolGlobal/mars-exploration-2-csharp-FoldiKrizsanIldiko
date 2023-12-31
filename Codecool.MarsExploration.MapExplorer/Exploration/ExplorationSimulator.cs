﻿using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.SimulationSteps;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.Output.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public class ExplorationSimulator
{
    private static SimulationContext? _simulationContext;
    private static readonly ILogger CLogger= new ConsoleLogger();
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    private static ILogger _fLogger = new FileLogger($@"{WorkDir}\Output\log.txt");
    private  ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();

    public ExplorationSimulator(Config configuration)
    {
        Config config = configuration;
        RoverDeployer roverDeployer = new RoverDeployer(config);
        _simulationContext = new SimulationContext(1,roverDeployer.Rover, config);
    }

    public void RunSimulation()
    {   
        for (int i = 1; i <= _simulationContext.TimeOutSteps; i++)
        {
            Movement.MoveTheRover(_simulationContext);
            StepIncrement();
            ScanResources.ScanResource(_coordinateCalculator, _simulationContext);
            
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
        CLogger.Log($" {_simulationContext.MarsRover} {_simulationContext.NumberOfSteps} step");
        _fLogger.Log($" {_simulationContext.MarsRover} {_simulationContext.NumberOfSteps} step");
    }

    public static void LoggingTheStep(string typeofEvent)
    {
        CLogger.Log($" {typeofEvent} {_simulationContext.MarsRover} ");
        _fLogger.Log($" {typeofEvent} {_simulationContext.MarsRover} ");
    }

    public void StepIncrement()
    {
        _simulationContext.NumberOfSteps++;
    }
}