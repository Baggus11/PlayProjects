using System;
using System.Collections.Generic;
using System.Linq;

namespace A_StarExample
{
    public class Program
    {
        private bool[,] map;
        private SearchParameters searchParameters;

        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            // Start with a clear map (don't add any obstacles)
            InitializeMap();
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> path = pathFinder.FindPath();
            ShowRoute("The algorithm should find a direct path without obstacles:", path);
            Console.WriteLine();

            // Now add an obstacle
            InitializeMap();
            AddWallWithGap();
            pathFinder = new PathFinder(searchParameters);
            path = pathFinder.FindPath();
            ShowRoute("The algorithm should find a route around the obstacle:", path);
            Console.WriteLine();

            // Finally, create a barrier between the start and end points
            InitializeMap();
            AddWallWithoutGap();
            pathFinder = new PathFinder(searchParameters);
            path = pathFinder.FindPath();
            ShowRoute("The algorithm should not be able to find a route around the barrier:", path);
            Console.WriteLine();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private void ShowRoute(string title, IEnumerable<Point> path)
        {
            Console.WriteLine("{0}\r\n", title);
            for (int y = map.GetLength(1) - 1; y >= 0; y--) // Invert the Y-axis so that coordinate 0,0 is shown in the bottom-left
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (searchParameters.StartLocation.Equals(new Point(x, y)))
                        // Show the start position
                        Console.Write('S');
                    else if (searchParameters.EndLocation.Equals(new Point(x, y)))
                        // Show the end position
                        Console.Write('F');
                    else if (map[x, y] == false)
                        // Show any barriers
                        Console.Write('░');
                    else if (path.Where(p => p.X == x && p.Y == y).Any())
                        // Show the path in between
                        Console.Write('*');
                    else
                        // Show nodes that aren't part of the path
                        Console.Write('·');
                }

                Console.WriteLine();
            }
        }

        private void InitializeMap()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            map = new bool[7, 5];
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 7; x++)
                    map[x, y] = true;

            var startLocation = new Point(1, 2);
            var endLocation = new Point(5, 2);
            searchParameters = new SearchParameters(startLocation, endLocation, map);
        }

        private void AddWallWithGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □

            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2

            map[3, 4] = false;
            map[3, 3] = false;
            map[3, 2] = false;
            map[3, 1] = false;
            map[4, 1] = false;
        }

        private void AddWallWithoutGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □

            // No path

            map[3, 4] = false;
            map[3, 3] = false;
            map[3, 2] = false;
            map[3, 1] = false;
            map[3, 0] = false;
        }

    }

}
