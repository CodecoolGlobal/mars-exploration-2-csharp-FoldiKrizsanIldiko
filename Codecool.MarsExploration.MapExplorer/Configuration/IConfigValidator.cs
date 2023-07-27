using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration;

public interface IConfigValidator
{
    bool IsConfigValid(Map map);
}