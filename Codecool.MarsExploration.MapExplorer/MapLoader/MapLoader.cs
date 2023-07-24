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
            List<string> characters = new List<string>();
            for (int i = 0; i < firstLine.Length; i++)
            {
                characters.Add(firstLine[i].ToString());
            }

            for (int i = 0; i < characters.Count; i++)
            {
                mapInString[rowCounter, i] = characters[i];
            }
            rowCounter++;

            // Process the rest of the lines
            string loadedMap;
            while ((loadedMap = _streamReader.ReadLine()) != null)
            {
                characters.Clear();
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

            return new Map(mapInString, true);
        }
    }
    // public Map Load(string mapFile)
    // {
    //     StreamReader getLenghtOfArray = new StreamReader(mapFile);
    //     StreamReader _streamReader = new StreamReader(mapFile);
    //     
    //     int rowCounter = 0;
    //     
    //     var lenghtOfArray = _streamReader.ReadLine();
    //     _streamReader.Close();
    //     
    //     string[,] mapInString = new string[lenghtOfArray.Length,lenghtOfArray.Length];
    //     while (_streamReader.ReadLine()!= null)
    //     {
    //         var loadedMap = _streamReader.ReadLine();
    //         List<string> characters = new List<string>() ;
    //         for (int i = 0; i < loadedMap.Length; i++)
    //         {
    //             characters.Add(loadedMap[i].ToString());
    //         }
    //         
    //         characters.ToList().ForEach(e => Console.Write(e));
    //         Console.WriteLine();
    //         for (int i = 0; i < characters.Count; i++)
    //         {
    //             mapInString[rowCounter,i] = characters[i];
    //         }
    //         rowCounter++;
    //     }
    //
    //     Console.WriteLine();
    //     return new Map(mapInString, true);
    // }
}