using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration;

public record Config(string path, Coordinate landingPoint, IEnumerable<string> resources, int steps);