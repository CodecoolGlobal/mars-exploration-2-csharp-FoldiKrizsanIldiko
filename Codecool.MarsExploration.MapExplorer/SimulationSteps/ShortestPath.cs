using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.SimulationSteps;

public class ShortestPath
{
    static int[] dx = { -1, 0, 1, 0 };
    static int[] dy = { 0, 1, 0, -1 };

    static bool IsValid(int x, int y, int rows, int cols)
    {
        return x >= 0 && x < rows && y >= 0 && y < cols;
    }

    public List<Coordinate> FindShortestPath(string[,] map, Coordinate start, Coordinate destination)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        bool[,] visited = new bool[rows, cols];
        Coordinate[,] previous = new Coordinate[rows, cols];

        Queue<Coordinate> queue = new Queue<Coordinate>();
        queue.Enqueue(start);
        visited[start.X, start.Y] = true;

        while (queue.Count > 0)
        {
            Coordinate current = queue.Dequeue();

            if (current.Equals(destination))
                break;

            for (int i = 0; i < 4; i++)
            {
                int newX = current.X + dx[i];
                int newY = current.Y + dy[i];

                if (IsValid(newX, newY, rows, cols) && !visited[newX, newY] && (map[newX, newY] == "A"||map[newX, newY] == "C"))
                {
                    visited[newX, newY] = true;
                    previous[newX, newY] = current;
                    queue.Enqueue(new Coordinate(newX, newY));
                }
            }
        }

        List<Coordinate> path = new List<Coordinate>();
        Coordinate currentStep = destination;

        while (currentStep != null)
        {
            path.Add(currentStep);
            currentStep = previous[currentStep.X, currentStep.Y];
        }

       
        for (int i = 0; i < path.Count-1; i++)
        {
            map[path[i].X, path[i].Y] = "🔛";
        }
      
        return path;
    }
}