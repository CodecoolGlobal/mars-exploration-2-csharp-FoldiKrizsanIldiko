using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;

namespace TestMapExplorer;

[TestFixture]
public class ConfigValidatorTest
{
    public static IEnumerable<TestCaseData> SourcePathAndLandCoordinates()
    {
        var workDir = AppDomain.CurrentDomain.BaseDirectory;

        yield return new TestCaseData($"{workDir}\\FakeMap.map", new Coordinate(1, 1), new List<string>()
            { "#", "&", "*", "%" }, 100).Returns(true); //good data
        yield return
            new TestCaseData($"{workDir}\\FakeMap.map", new Coordinate(0, 0), new List<string>()
                    { "#", "&", "*", "%" }, 100)
                .Returns(false); //no free landing coordinate
        yield return
            new TestCaseData($"{workDir}\\FakeMap.map", new Coordinate(1, 5), new List<string>()
                { "#", "&", "*", "%" }, 100).Returns(false); //no free for rover
        yield return
            new TestCaseData($"{workDir}\\Usings.cs", new Coordinate(1, 5), new List<string>()
                { "#", "&", "*", "%" }, 100).Returns(false); //.cs file not .map
        yield return
            new TestCaseData($"{workDir}\\FakeMap1.map", new Coordinate(1, 5), new List<string>()
                { "#", "&", "*", "%" }, 100).Returns(false); //file not exists
        yield return new TestCaseData($"{workDir}\\FakeMap.map", new Coordinate(1, 1), new List<string>()
            { "#", "&", "*", "%" }, 0).Returns(false); //steps 0
        yield return new TestCaseData($"{workDir}\\FakeMap.map", new Coordinate(1, 1), new List<string>()
            { }, 100).Returns(false); //empty resources
    }

    [TestCaseSource(nameof(SourcePathAndLandCoordinates))]
    public bool ConfigValidatorWithGoodInput(string filePath, Coordinate landingPoint, List<string> resources,
        int steps)
    {
        //arrange
        Config testConfig = new Config(filePath, landingPoint, resources, steps);
        IConfigValidator _configValidator = new ConfigValidator(testConfig);
        //act
        return _configValidator.IsConfigValid();
    }
}