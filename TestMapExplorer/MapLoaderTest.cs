using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace TestMapExplorer;

public class MapLoaderTest
{
    [Test]
    public void TestMapEquality()
    {
        //arrange
        string[,] map = new string[,]
        {
            { "1"," "," "," ", "1"," "," "," ", "1" },
            {" "," "," ","1"," ","1"," "," "," ",},
            {" "," "," "," ","1"," "," "," "," ",},
            {" "," "," "," "," "," "," "," "," ",},
            { "1"," "," "," ", "1"," "," "," ", "1" },
            {" "," "," "," "," "," "," "," "," ",},
            {" "," "," "," "," "," "," "," "," ",},
            {" "," "," "," "," "," "," "," "," ",},
            { "1"," "," "," ", "1"," "," "," ", "1" },
        };
        Map testMap = new Map(map, true);
    //    Map testMap1 = new Map(map, true);
        IMapLoader mapLoader = new MapLoader();

        //act
        var workDir = AppDomain.CurrentDomain.BaseDirectory;
        Map result = mapLoader.Load($"{workDir}\\FakeMap.map");
        //assert
      //  Assert.That(testMap, Is.EqualTo(result));
     
      var comparer = new Map.MapEqualityComparer();
      
       Assert.That(comparer.Equals(testMap, result), Is.True);
        Assert.That(result.Representation, Is.EqualTo(testMap.Representation));
        Assert.That(result.SuccessfullyGenerated, Is.EqualTo(testMap.SuccessfullyGenerated));
    }
}