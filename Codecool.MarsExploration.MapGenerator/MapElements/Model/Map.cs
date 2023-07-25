using System.Text;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapGenerator.MapElements.Model;

public record Map(string?[,] Representation, bool SuccessfullyGenerated = false)
{
    public int Dimension => Representation.GetLength(0);

    private static string CreateStringRepresentation(string?[,] arr)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < arr.GetLength(0); i++)
        {
            string s = "";
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                s += arr[i, j] == null ? " " : arr[i, j];
            }

            sb.AppendLine(s);
        }

        return sb.ToString();
    }

    public string? GetByCoordinate(Coordinate coordinate)
    {
        return Representation[coordinate.X, coordinate.Y];
    }

    public bool IsEmpty(Coordinate coordinate)
    {
        return string.IsNullOrEmpty(Representation[coordinate.X, coordinate.Y])
            || Representation[coordinate.X, coordinate.Y] == " ";
    }

    public override string ToString()
    {
        return CreateStringRepresentation(Representation);
    }

    // private sealed class MapEqualityComparer : IEqualityComparer<Map>
    // {
    //     public bool Equals(Map x, Map y)
    //     {
    //         if (ReferenceEquals(x, y)) return true;
    //         if (ReferenceEquals(x, null)) return false;
    //         if (ReferenceEquals(y, null)) return false;
    //         if (x.GetType() != y.GetType()) return false;
    //         return x.Representation.Equals(y.Representation) && x.SuccessfullyGenerated == y.SuccessfullyGenerated;
    //     }
    //
    //     public int GetHashCode(Map obj)
    //     {
    //         return HashCode.Combine(obj.Representation, obj.SuccessfullyGenerated);
    //     }
    // }
    //
    // public static IEqualityComparer<Map> MapComparer { get; } = new MapEqualityComparer();
}
