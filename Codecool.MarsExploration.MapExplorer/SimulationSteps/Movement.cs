using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.SimulationSteps;

public static class Movement
{
 
    private static readonly ICoordinateCalculator CoordinateCalculator = new CoordinateCalculator();
    private static readonly Random Random = new();
    
    public static void MoveTheRover(SimulationContext simulationContext)
    {
        Coordinate nextStep;
        Map marsMap = simulationContext.Map;
        Coordinate roversCurrentPosition = simulationContext.MarsRover.CurrentPosition;
        
        var wasThere=  simulationContext.MarsRover.RoadTaken;
        var emptyAdjacentFields =
            FindEmptyAdjacentField.FindEmptyAdjacentFields(CoordinateCalculator, marsMap, roversCurrentPosition, simulationContext.LocationOfShip).ToList();
        
        var unvisitedEmptyAdjacentFields = emptyAdjacentFields.Where(e => !wasThere.Contains(e)).ToList();
    
        if (unvisitedEmptyAdjacentFields.Any())
        {
            nextStep = unvisitedEmptyAdjacentFields[Random.Next(unvisitedEmptyAdjacentFields.Count)]; 
        }
        else
        {
            nextStep = emptyAdjacentFields[Random.Next(emptyAdjacentFields.Count())];
        }
        
        simulationContext.MarsRover.RoadTaken.Add(roversCurrentPosition);
        simulationContext.Map.Representation[simulationContext.LocationOfShip.X, simulationContext.LocationOfShip.Y] = "B";
        simulationContext.Map.Representation[roversCurrentPosition.X, roversCurrentPosition.Y] = "A";
        simulationContext.Map.Representation[nextStep.X, nextStep.Y] = "A";
        simulationContext.MarsRover.CurrentPosition = nextStep;
        ExplorationSimulator.LoggingTheStep();
    }

    public static void BackToTheShip(SimulationContext simulationContext)
    {
        simulationContext.MarsRover.CurrentPosition = simulationContext.LocationOfShip;
        ExplorationSimulator.LoggingTheStep("Teleported back to the Ship!");
    }
}