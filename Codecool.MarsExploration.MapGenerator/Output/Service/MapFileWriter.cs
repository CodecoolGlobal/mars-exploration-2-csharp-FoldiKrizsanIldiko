using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapGenerator.Output.Service;

public class MapFileWriter : IMapFileWriter
{
    public void WriteMapFile(Map map, string file)
    {
        Dictionary<string, string> symbols = new Dictionary<string, string>()
        {
            { "#", "#" },
            { "&", "&" },
            { "*", "*" },
            { "%", "%" },
            { " ", " " },
            { "|", " " },
            { "/", " " }
            // { "#", "🏔️" },
            // { "&", "🕳️" },
            // { "*", "💎" },
            // { "%", "🌊" },
            // { " ", "🟥" },
            // { "|", "🟫" },
            // { "/", "🟩" }
        };
        for (int i = 0; i < map.Representation.GetLength(0); i++)
        {
            for (int j = 0; j < map.Representation.GetLength(1); j++)
            {
               if (symbols.TryGetValue(map.Representation[i, j], out string value))
               {
                    map.Representation[i, j] = value;
               }
            }
        }
        File.WriteAllText(file, map.ToString());
    }
}
