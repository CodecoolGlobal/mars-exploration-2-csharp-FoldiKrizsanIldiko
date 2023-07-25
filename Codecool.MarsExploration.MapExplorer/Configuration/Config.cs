using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration;

public record Config(string filepath, Coordinate landingPoint, IEnumerable<string> resources, int steps);