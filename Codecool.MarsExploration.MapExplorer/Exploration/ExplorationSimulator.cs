using System.Runtime.InteropServices.ComTypes;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

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
    public  SimulationContext _simulationContext;
    private static ILogger _cLogger= new ConsoleLogger();
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    private static ILogger _fLogger = new FileLogger($@"{WorkDir}\Output\log.txt");// constructoron kívül példányosítani
    private  ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();
    private  Random _random = new Random();

    public ExplorationSimulator(Config config)
    {
        _config = config;
        _roverDeployer = new RoverDeployer(_config);
        _simulationContext = new SimulationContext(0,_roverDeployer.Rover, _config);
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

    // Movement. The rover needs to determine an adjacent empty spot of the chart to move
    //
    // Scanning. The rover needs to scan the area for resources based on how far it can see (its sight).
    //
    // Analysis. After the information is gathered, you need to determine whether an outcome is reached.
    //
    //     Log. Write the current state of events in the simulation to the log file.//
    //
    //     Step increment. Increment the context step variable by one.//

    public void Movement()
    {
        //landing spot legyen egy szimbólummal ellátva, és az legyen kizárva hogy oda nem léphet
        Coordinate MyChosedNextStep;
        Map myMap = _simulationContext.Map;
        Coordinate RoversCurrentPosition = _simulationContext.MarsRover.CurrentPosition;
       var wasThere=  _simulationContext.MarsRover.RoadTaken;
        var emptyAdjacentFields =
            FindEmptyAdjacentField.FindEmptyAdjacentFields(_coordinateCalculator, myMap, RoversCurrentPosition, _config.landingPoint).ToList();
        Console.WriteLine();
       // var nextStep = emptyAdjacentFields.ToList()[_random.Next(emptyAdjacentFields.Count())];
        var PossibleMovesIWasNotThereYet = emptyAdjacentFields.Where(e => !wasThere.Contains(e)).ToList();
        
        if (PossibleMovesIWasNotThereYet.Any())
        {
            
           MyChosedNextStep = PossibleMovesIWasNotThereYet[_random.Next(PossibleMovesIWasNotThereYet.Count)]; 
        }
        else
        {
            MyChosedNextStep = emptyAdjacentFields[_random.Next(emptyAdjacentFields.Count())];
        }
       // Rover is moving
       
       _simulationContext.MarsRover.RoadTaken.Add(RoversCurrentPosition);
       _simulationContext.Map.Representation[_config.landingPoint.X, _config.landingPoint.Y] = "B";//űrhajó berajzolása
       _simulationContext.Map.Representation[RoversCurrentPosition.X, RoversCurrentPosition.Y] = "A";//nem rakta rá a térképre az első poziciót
       _simulationContext.Map.Representation[MyChosedNextStep.X, MyChosedNextStep.Y] = "A";
       _simulationContext.MarsRover.CurrentPosition = MyChosedNextStep;
       LoggingTheStep();
    }
    public void ScanningTheSightArea()
    {
        //nézzen szét és nézze meg hol van resource, jegyezze meg
        //a rover Dictionary adatait kell itt feltölteni - hozzáadni
    }

    public bool Analyser()
    {
        //what will be the feltétel
        //a rover Dictionaryjából szedi az adatokat
        //minden lépés után nézze meg talált-e amit keresett
        //nézze meg van-e még lépése?
        throw new NotImplementedException();
    }

    public void LoggingTheStep() // írja hol tart
    {
        _cLogger.Log($" [{_simulationContext.MarsRover}] {_simulationContext.NumberOfSteps} step");
        _fLogger.Log($" [{_simulationContext.MarsRover}] {_simulationContext.NumberOfSteps} step");
    }

    public void LoggingTheStep(string outcome)// ha eredmény van akkor írja az eredményt
    {
        _cLogger.Log($" [{_simulationContext.MarsRover}] {outcome}");
        _fLogger.Log($" [{_simulationContext.MarsRover}] {outcome}");
    }

    public void StepIncrement()
    {
        _simulationContext.NumberOfSteps++;
    }
}