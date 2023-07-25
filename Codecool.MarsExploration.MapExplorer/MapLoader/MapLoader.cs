using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MapLoader;

public class MapLoader : IMapLoader
{
    
    public Map Load(string mapFile)
    {
        using (StreamReader _streamReader = new StreamReader(mapFile))
        {
            int rowCounter = 0;

            string firstLine = _streamReader.ReadLine();
            if (firstLine == null)
            {
                // Handle the case where the file is empty or contains no lines
                return null;
            }

            int arrayLength = firstLine.Length;
            string[,] mapInString = new string[arrayLength, arrayLength];

            // Process the first line
            List<string> characters = new List<string>();//first line of map separated to strings
            FillByLine(firstLine, characters, mapInString,ref rowCounter);

            // Process the rest of the lines
            string loadedMap;
            while ((loadedMap = _streamReader.ReadLine()) != null)
            {
                characters.Clear();
                FillByLine(loadedMap, characters, mapInString, ref rowCounter);
            }

            // Console.WriteLine(mapInString.Length);
            return new Map(mapInString, true);
        }
    }

    private void FillByLine(string loadedMap, List<string> characters, string[,] mapInString,ref int rowCounter)
    {
        for (int i = 0; i < loadedMap.Length; i++)
        {
            characters.Add(loadedMap[i].ToString());
        }
        // characters.ToList().ForEach(e => Console.Write(e));
        // Console.WriteLine();
        for (int i = 0; i < characters.Count; i++)
        {
            mapInString[rowCounter, i] = characters[i];
        }
        rowCounter++;
    }
}