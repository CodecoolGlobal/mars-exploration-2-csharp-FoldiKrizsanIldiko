using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class MarsRover
{
    public string Id { get; init; }
    public Coordinate CurrentPosition { get; set; }
    public int SightDistance { get; init; }
    private Dictionary<string, List<Coordinate>> EncounteredResources { get; set; }

    private readonly int _roverCounter = 1;

    public MarsRover(Coordinate currentPosition, int sightDistance = 2)
    {
        Id = $"Rover-{_roverCounter}";
        CurrentPosition = currentPosition;
        SightDistance = sightDistance;
        EncounteredResources = new Dictionary<string, List<Coordinate>>();
    }

    public override string ToString()
    {
        return $"{Id} is on the [{CurrentPosition.X}, {CurrentPosition.Y}] coordinate.";
    }
}