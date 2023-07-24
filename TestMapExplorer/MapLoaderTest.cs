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
            { "1", "1", "1" },
            { "1", "1", "1" },
            { "1", "1", "1" }
        };
        Map testMap = new Map(map, true);
        IMapLoader mapLoader = new MapLoader();

        //act
        var workDir = AppDomain.CurrentDomain.BaseDirectory;
        Map result = mapLoader.Load($"{workDir}\\FakeMap.map");
        //assert

        Assert.AreEqual(testMap.Representation, result.Representation);
        Assert.AreEqual(testMap.SuccessfullyGenerated, result.SuccessfullyGenerated);
    }
}