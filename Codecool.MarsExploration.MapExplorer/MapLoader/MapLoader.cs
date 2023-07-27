using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MapLoader;

public class MapLoader : IMapLoader
{
    public Map Load(string mapFile)
    {
        using (StreamReader streamReader = new StreamReader(mapFile))
        {
            int rowCounter = 0;

            string firstLine = streamReader.ReadLine();
            if (firstLine == null)
            {
                return null;
            }

            int arrayLength = firstLine.Length;
            string[,] mapInString = new string[arrayLength, arrayLength];

            List<string> characters = new List<string>();
            FillByLine(firstLine, characters, mapInString,ref rowCounter);

            string loadedMap;
            while ((loadedMap = streamReader.ReadLine()) != null)
            {
                characters.Clear();
                FillByLine(loadedMap, characters, mapInString, ref rowCounter);
            }
            
            return new Map(mapInString, true);
        }
    }

    private void FillByLine(string loadedMap, List<string> characters, string[,] mapInString,ref int rowCounter)
    {
        for (int i = 0; i < loadedMap.Length; i++)
        {
            characters.Add(loadedMap[i].ToString());
        }

        for (int i = 0; i < characters.Count; i++)
        {
            mapInString[rowCounter, i] = characters[i];
        }
        rowCounter++;
    }
}