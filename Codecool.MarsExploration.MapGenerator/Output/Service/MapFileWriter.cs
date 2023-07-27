using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapGenerator.Output.Service;

public class MapFileWriter : IMapFileWriter
{
    public void WriteMapFile(Map map, string file)
    {
        Dictionary<string, string> symbols = new Dictionary<string, string>()
        {
            // { "#", "#" },
            // { "&", "&" },
            // { "*", "*" },
            // { "%", "%" },
            // { " ", " " },
            // { "|", " " },
            // { "/", " " }
            { "#", "🏔️" },
            { "&", "🕳️" },
            { "*", "💎" },
            { "%", "🌊" },
            { " ", "🟥" },
            { "|", "🟫" },
            { "/", "🟩" },
            {"A", "🟡"},
            {"B","🛸"},
            {"C","👀"},

        };
        
        Map innerMap = new Map(CreateDeepCopy(map.Representation), true);
        for (int i = 0; i < innerMap.Representation.GetLength(0); i++)
        {
            for (int j = 0; j < innerMap.Representation.GetLength(1); j++)
            {
               if (symbols.TryGetValue(innerMap.Representation[i, j], out string value))
               {
                    innerMap.Representation[i, j] = value;
               }
            }
        }
        File.WriteAllText(file, innerMap.ToString());
    }
    
    private string[,] CreateDeepCopy(string[,] source)
    {
        int rows = source.GetLength(0);
        int cols = source.GetLength(1);
        string[,] copy = new string[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                copy[i, j] = source[i, j];
            }
        }

        return copy;
    }
}
