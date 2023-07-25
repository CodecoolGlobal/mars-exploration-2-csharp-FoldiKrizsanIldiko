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
      Console.WriteLine("LandingSpot is not empty or has no empty adjacent coordinate.");
         return false;
   }

   private bool IsFilePathValid()
   {
      if (File.Exists(_configuration.filepath) && Path.GetExtension(_configuration.filepath) == ".map")
      {
         return true;
      }
      Console.WriteLine("Filepath is invalid.");
      return false;
   }

   private bool AreResourcesSpecified()
   {
      if (_configuration.resources.Any())
      {
         return true;
      }

      Console.WriteLine("Resources are not specified.");
      return false;
   }

   private bool IsTimeoutValid()
   {
      if (_configuration.steps > 0)
      {
         return true;
      }
      Console.WriteLine("Number of steps is zero.");
      return false;
   }
   
   private bool HasEmptyAdjacent(Coordinate coordinate, string?[,] map)
   {
      var toReturn = _coordinateCalculator.GetAdjacentCoordinates(coordinate, map.GetLength(0))
         .Where(c => map[c.X, c.Y] == " ");
      // Console.WriteLine($"{toReturn.ToList().Count} {toReturn.ToList().Count >= 1}");
      return toReturn.ToList().Count >= 1;
   }
   
}