using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration;

public class ConfigValidator : IConfigValidator
{
   private readonly Config _configuration;
   private readonly Map _mapOfConfig;
   private readonly MapLoader.MapLoader _mapLoader = new ();
   private readonly ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();

   public ConfigValidator(Config configuration)
   {
      _configuration = configuration;
      _mapOfConfig = _mapLoader.Load(configuration.filepath);
   }

   public bool IsConfigValid()
   {
      return IsLandingSpotValid() && IsFilePathValid() && AreResourcesSpecified() && IsTimeoutValid();
   }
   
   private bool IsLandingSpotValid()
   {
      Coordinate landingSpot = _configuration.landingPoint;
      if (_mapOfConfig.IsEmpty(landingSpot) && HasEmptyAdjacent(landingSpot, _mapOfConfig.Representation))
      {
         return true;
      }
      Console.WriteLine("landingSpot is not empty or has no empty adjacent coordinate.");
         return false;
   }

   private bool IsFilePathValid()
   {
      return _configuration.filepath.Length > 0;
   }

   private bool AreResourcesSpecified()
   {
      return _configuration.resources.Any();
   }

   private bool IsTimeoutValid()
   {
      return _configuration.steps > 0;
   }
   
   private bool HasEmptyAdjacent(Coordinate coordinate, string?[,] map)
   {
      var toReturn = _coordinateCalculator.GetAdjacentCoordinates(coordinate, map.GetLength(0))
         .Where(c => map[c.X, c.Y] == " ");
      // Console.WriteLine($"{toReturn.ToList().Count} {toReturn.ToList().Count >= 1}");
      return toReturn.ToList().Count >= 1;
   }
   
}