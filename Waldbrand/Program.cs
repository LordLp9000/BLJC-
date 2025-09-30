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
        static bool initialFireCreated = false;
        static int turnCounter = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

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

                turnCounter++;
                StartRandomFires();
                SpreadFires();
                UpdateBurntTrees();
                GrowNewTrees();
                DisplayForest();
                System.Threading.Thread.Sleep(1500);
            }
        }

        static void DisplayForest()
        {
            int maxDisplayWidth = Math.Min(width, Console.WindowWidth / 2);
            int maxDisplayHeight = Math.Min(height, Console.WindowHeight - 3);

            Console.SetCursorPosition(0, 0);

            for (int row = 0; row < maxDisplayHeight; row++)
            {
                for (int col = 0; col < maxDisplayWidth; col++)
                {
                    char cell = forest[row, col];
                    string displayChar;
                    switch (cell)
                    {
                        case 'F':
                            Console.ForegroundColor = ConsoleColor.Red;
                            displayChar = char.ConvertFromUtf32(0x1F525); // 🔥 
                            break;
                        case 'f':
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            displayChar = char.ConvertFromUtf32(0x1F4A8); // 💨 
                            break;
                        case 'B':
                            Console.ForegroundColor = ConsoleColor.Green;
                            displayChar = char.ConvertFromUtf32(0x1F332); // 🌲
                            break;
                        case 'S':
                            Console.ForegroundColor = ConsoleColor.Gray;
                            displayChar = char.ConvertFromUtf32(0x1FAA8); // 🪨 
                            break;
                        case '-':
                            Console.ForegroundColor = ConsoleColor.White;
                            displayChar = char.ConvertFromUtf32(0x2B1C); // ⬜ 
                            break;
                        default:
                            displayChar = cell.ToString();
                            break;
                    }
                    Console.Write(displayChar);
                }
                Console.ResetColor();

                int currentPosition = Console.CursorLeft;
                if (currentPosition < Console.WindowWidth)
                {
                    Console.Write(new string(' ', Console.WindowWidth - currentPosition));
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

            if (width > maxDisplayWidth || height > maxDisplayHeight)
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
            int existingFires = 0;
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (forest[row, col] == 'F')
                    {
                        existingFires++;
                    }
                }
            }

            if (existingFires == 0 && !initialFireCreated)
            {
                List<(int, int)> availableTrees = new List<(int, int)>();
                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        if (forest[row, col] == 'B')
                        {
                            availableTrees.Add((row, col));
                        }
                    }
                }

                if (availableTrees.Count > 0)
                {
                    int randomIndex = random.Next(availableTrees.Count);
                    var (fireRow, fireCol) = availableTrees[randomIndex];
                    forest[fireRow, fireCol] = 'F';
                    fireAge[fireRow, fireCol] = 0;
                    initialFireCreated = true;
                }
            }

            if (sparkProbability <= 1.0)
            {
                return;
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (forest[row, col] == 'B' && random.NextDouble() < sparkProbability / 100.0)
                    {
                        forest[row, col] = 'F';
                        fireAge[row, col] = 0;
                    }
                }
            }
        }

        static void SpreadFires()
        {
            if (sparkProbability <= 1.0 && turnCounter == 1)
            {
                return;
            }

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
                        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
                        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

                        for (int i = 0; i < 8; i++)
                        {
                            int neighborRow = row + dx[i];
                            int neighborCol = col + dy[i];

                            if (neighborRow >= 0 && neighborRow < height &&
                                neighborCol >= 0 && neighborCol < width &&
                                forest[neighborRow, neighborCol] == 'B')
                            {
                                newForest[neighborRow, neighborCol] = 'F';
                                newFireAge[neighborRow, neighborCol] = 0;
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

                        int burnDuration = random.Next(3, 7);

                        if (fireAge[row, col] >= burnDuration)
                        {
                            forest[row, col] = 'f';
                            emptyAge[row, col] = 0;
                        }
                    }
                    else if (forest[row, col] == 'f')
                    {
                        emptyAge[row, col]++;

                        int clearDuration = random.Next(1, 3);

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
                    if (forest[row, col] == '-'
                        && emptyAge[row, col] >= 4)
                    {
                        bool hasNearbyBurntTrees = false;
                        for (int dr = -1; dr <= 1 && !hasNearbyBurntTrees; dr++)
                        {
                            for (int dc = -1; dc <= 1 && !hasNearbyBurntTrees; dc++)
                            {
                                int checkRow = row + dr;
                                int checkCol = col + dc;

                                if (checkRow >= 0 && checkRow < height &&
                                    checkCol >= 0 && checkCol < width &&
                                    forest[checkRow, checkCol] == 'f')
                                {
                                    hasNearbyBurntTrees = true;
                                }
                            }
                        }

                        double adjustedGrowthProb = growthProbability / 10.0;
                        if (hasNearbyBurntTrees)
                        {
                            adjustedGrowthProb *= 4.0;
                        }

                        if (random.NextDouble() < adjustedGrowthProb)
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
