using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace TestMapExplorer;

public class MapLoaderTest
{
    [Test]
   public void TestCaracterNumbers()
   {
       string[,] map = new string[,]
       {
           { "1", "1", "1" },
           { "1", "1", "1" },
           { "1", "1", "1" }
       };
       Map testMap = new Map(map, true);
       
      //arrange
      IMapLoader mapLoader = new MapLoader();
      
      //act
     
       Map result = mapLoader.Load("C:\\Users\\fkild\\Codecool\\OOP_module\\4_TW\\mars-exploration-2-csharp-FoldiKrizsanIldiko\\TestMapExplorer\\FakeMap.map");

      //assert
      //egyforma-e a méretük
 
      Assert.AreEqual(testMap.Representation, result.Representation);
      Assert.AreEqual(testMap.SuccessfullyGenerated, result.SuccessfullyGenerated);
   }
}