using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class MarsRover
{
    public string Id { get; }
    public Coordinate CurrentPosition { get; set; }
    public int SightDistance { get; }
    public Dictionary<string, HashSet<Coordinate>> EncounteredResources { get; set; }

    private readonly int _roverCounter = 1;
    public readonly List<Coordinate> RoadTaken;

    public MarsRover(Coordinate currentPosition, int sightDistance = 2)
    {
        Id = $"Rover-{_roverCounter++}";
        CurrentPosition = currentPosition;
        SightDistance = sightDistance;
        EncounteredResources = new Dictionary<string, HashSet<Coordinate>>{
            {"#", new HashSet<Coordinate>()},
            {"&", new HashSet<Coordinate>()},
            {"*", new HashSet<Coordinate>()},
            {"%", new ()}
        };
        RoadTaken = new List<Coordinate>();
    }

    public override string ToString()
    {
        return $"{Id} on [{CurrentPosition.X}, {CurrentPosition.Y}] coordinate.";
    }
}