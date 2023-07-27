using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.SimulationSteps;

public static class Movement
{
 
    private static ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();
    private static Random _random = new();
    
    public static void MoveTheRover(SimulationContext simulationContext)
    {
        Coordinate NextStep;
        Map marsMap = simulationContext.Map;
        Coordinate RoversCurrentPosition = simulationContext.MarsRover.CurrentPosition;
        var wasThere=  simulationContext.MarsRover.RoadTaken;
        var emptyAdjacentFields =
            FindEmptyAdjacentField.FindEmptyAdjacentFields(_coordinateCalculator, marsMap, RoversCurrentPosition, simulationContext.LocationOfShip).ToList();
        Console.WriteLine();
        var unvisitedEmptyAdjacentFields = emptyAdjacentFields.Where(e => !wasThere.Contains(e)).ToList();
    
        if (unvisitedEmptyAdjacentFields.Any())
        {
            NextStep = unvisitedEmptyAdjacentFields[_random.Next(unvisitedEmptyAdjacentFields.Count)]; 
        }
        else
        {
            NextStep = emptyAdjacentFields[_random.Next(emptyAdjacentFields.Count())];
        }
        // Rover is moving
        simulationContext.MarsRover.RoadTaken.Add(RoversCurrentPosition);
        simulationContext.Map.Representation[simulationContext.LocationOfShip.X, simulationContext.LocationOfShip.Y] = "B";//spaceship place on map
        simulationContext.Map.Representation[RoversCurrentPosition.X, RoversCurrentPosition.Y] = "A";//first position of Rover place on map
        simulationContext.Map.Representation[NextStep.X, NextStep.Y] = "A";
        simulationContext.MarsRover.CurrentPosition = NextStep;
        ExplorationSimulator.LoggingTheStep();
    }

    public static void BackToTheShip(SimulationContext simulationContext)
    {
        simulationContext.MarsRover.CurrentPosition = simulationContext.LocationOfShip;
        ExplorationSimulator.LoggingTheStep("Teleported back to the Ship!");
    }
}