using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaldBrand
{
    internal class Program
    {

        static char[,] forest;
        static int[,] fireAge;
        static int[,] emptyAge;
        static int width;
        static int height;
        static double sparkProbability;
        static double growthProbability;
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Enter forest width:");
            width = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter forest height:");
            height = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter spark probability (0-10):");
            sparkProbability = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter growth probability (0-10):");
            growthProbability = double.Parse(Console.ReadLine());

            forest = new char[height, width];
            fireAge = new int[height, width];
            emptyAge = new int[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    double randomValue = random.NextDouble();
                    if (randomValue < 0.6)
                    {
                        forest[row, col] = 'B';
                    }
                    else if (randomValue < 0.8)
                    {
                        forest[row, col] = 'S';
                    }
                    else
                    {
                        forest[row, col] = '-';
                        emptyAge[row, col] = 0;
                    }
                    fireAge[row, col] = 0;
                }
            }

            Console.Clear();
            Console.Title = "Waldbrand Simulation";

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    break;
                }

                StartRandomFires();
                SpreadFires();
                UpdateBurntTrees();
                GrowNewTrees();
                DisplayForest();
                System.Threading.Thread.Sleep(500);
            }
        }

        static void DisplayForest()
        {
            int maxDisplayWidth = Math.Min(width, Console.WindowWidth);
            int maxDisplayHeight = Math.Min(height, Console.WindowHeight - 3);

            Console.SetCursorPosition(0, 0);

            for (int row = 0; row < maxDisplayHeight; row++)
            {
                for (int col = 0; col < maxDisplayWidth; col++)
                {
                    char cell = forest[row, col];
                    switch (cell)
                    {
                        case 'F':
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case 'f':
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        case 'B':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 'S':
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case '-':
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.Write(cell);
                }
                Console.ResetColor();

                if (maxDisplayWidth < Console.WindowWidth)
                {
                    Console.Write(new string(' ', Console.WindowWidth - maxDisplayWidth));
                }

                if (row < maxDisplayHeight - 1)
                {
                    Console.WriteLine();
                }
            }

            for (int row = maxDisplayHeight; row < Console.WindowHeight - 3; row++)
            {
                Console.SetCursorPosition(0, row);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.WindowHeight - 3);

            if (width > Console.WindowWidth || height > Console.WindowHeight - 3)
            {
                Console.Write($"Showing {maxDisplayWidth}x{maxDisplayHeight} of {width}x{height} forest");
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Press any key to quit.");
        }

        static void StartRandomFires()
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (forest[row, col] == 'B' && random.NextDouble() < sparkProbability / 10.0)
                    {
                        forest[row, col] = 'F';
                        fireAge[row, col] = 0;
                    }
                }
            }
        }

        static void SpreadFires()
        {
            char[,] newForest = new char[height, width];
            int[,] newFireAge = new int[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    newForest[row, col] = forest[row, col];
                    newFireAge[row, col] = fireAge[row, col];
                }
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (forest[row, col] == 'F')
                    {
                        List<(int, int)> neighbors = new List<(int, int)>();
                        for (int dr = -1; dr <= 1; dr++)
                        {
                            for (int dc = -1; dc <= 1; dc++)
                            {
                                if (dr == 0 && dc == 0) continue;

                                int neighborRow = row + dr;
                                int neighborCol = col + dc;

                                if (neighborRow >= 0 && neighborRow < height &&
                                    neighborCol >= 0 && neighborCol < width)
                                {
                                    neighbors.Add((neighborRow, neighborCol));
                                }
                            }
                        }

                        for (int i = neighbors.Count - 1; i > 0; i--)
                        {
                            int j = random.Next(i + 1);
                            var temp = neighbors[i];
                            neighbors[i] = neighbors[j];
                            neighbors[j] = temp;
                        }

                        foreach (var (neighborRow, neighborCol) in neighbors)
                        {
                            if (forest[neighborRow, neighborCol] == 'B')
                            {
                                double spreadChance = 0.7;

                                bool isDiagonal = Math.Abs(neighborRow - row) == 1 && Math.Abs(neighborCol - col) == 1;
                                if (isDiagonal)
                                {
                                    spreadChance *= 0.6;
                                }

                                if (random.NextDouble() < spreadChance)
                                {
                                    newForest[neighborRow, neighborCol] = 'F';
                                    newFireAge[neighborRow, neighborCol] = 0;
                                }
                            }
                        }
                    }
                }
            }

            forest = newForest;
            fireAge = newFireAge;
        }

        static void UpdateBurntTrees()
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (forest[row, col] == 'F')
                    {
                        fireAge[row, col]++;

                        int burnDuration = random.Next(2, 5);

                        if (fireAge[row, col] >= burnDuration)
                        {
                            forest[row, col] = 'f';
                            emptyAge[row, col] = 0;
                        }
                    }
                    else if (forest[row, col] == 'f')
                    {
                        emptyAge[row, col]++;

                        int clearDuration = random.Next(3, 7);

                        if (emptyAge[row, col] >= clearDuration)
                        {
                            forest[row, col] = '-';
                            emptyAge[row, col] = 0;
                        }
                    }
                    else if (forest[row, col] == '-')
                    {
                        emptyAge[row, col]++;
                    }
                }
            }
        }

        static void GrowNewTrees()
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (forest[row, col] == '-')
                    {
                        int minGrowthTime = random.Next(1, 4);

                        bool hasNearbyTrees = false;
                        for (int dr = -2; dr <= 2 && !hasNearbyTrees; dr++)
                        {
                            for (int dc = -2; dc <= 2 && !hasNearbyTrees; dc++)
                            {
                                int checkRow = row + dr;
                                int checkCol = col + dc;

                                if (checkRow >= 0 && checkRow < height &&
                                    checkCol >= 0 && checkCol < width &&
                                    forest[checkRow, checkCol] == 'B')
                                {
                                    hasNearbyTrees = true;
                                }
                            }
                        }

                        double adjustedGrowthProb = growthProbability / 10.0;
                        if (hasNearbyTrees)
                        {
                            adjustedGrowthProb *= 1.5;
                        }
                        else
                        {
                            adjustedGrowthProb *= 0.3;
                        }

                        if (emptyAge[row, col] >= minGrowthTime && random.NextDouble() < adjustedGrowthProb)
                        {
                            forest[row, col] = 'B';
                            emptyAge[row, col] = 0;
                        }
                    }
                }
            }
        }
    }
}
