using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.SimulationSteps;

public class Movement
{
    public static SimulationContext SimulationContext;
    private static Config _config; 
    private static ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();
    private static Random _random = new();
    
    public static void MoveTheRover()
    {
        Coordinate MyChosedNextStep;
            Map myMap = SimulationContext.Map;
            Coordinate RoversCurrentPosition = SimulationContext.MarsRover.CurrentPosition;
            var wasThere=  SimulationContext.MarsRover.RoadTaken;
            var emptyAdjacentFields =
                FindEmptyAdjacentField.FindEmptyAdjacentFields(_coordinateCalculator, myMap, RoversCurrentPosition, _config.landingPoint).ToList();
            Console.WriteLine();
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
            SimulationContext.MarsRover.RoadTaken.Add(RoversCurrentPosition);
            SimulationContext.Map.Representation[_config.landingPoint.X, _config.landingPoint.Y] = "B";//spaceship place on map
            SimulationContext.Map.Representation[RoversCurrentPosition.X, RoversCurrentPosition.Y] = "A";//first position of Rover place on map
            SimulationContext.Map.Representation[MyChosedNextStep.X, MyChosedNextStep.Y] = "A";
            SimulationContext.MarsRover.CurrentPosition = MyChosedNextStep;
            ExplorationSimulator.LoggingTheStep();
        
    }
}