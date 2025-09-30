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
                    }
                }
            }

            Console.Clear();
            Console.Title = "Waldbrand Simulation, created 2017 by umu.ch";
            
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
                System.Threading.Thread.Sleep(300);
            }
        }

        static void DisplayForest()
        {
            Console.SetCursorPosition(0, 0);
            
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    Console.Write(forest[row, col]);
                }
                if (row < height - 1) Console.WriteLine();
            }
            
            Console.WriteLine();
            Console.WriteLine();
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
                    }
                }
            }
        }

        static void SpreadFires()
        {
            char[,] newForest = new char[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    newForest[row, col] = forest[row, col];
                }
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (forest[row, col] == 'F')
                    {
                        for (int dr = -1; dr <= 1; dr++)
                        {
                            for (int dc = -1; dc <= 1; dc++)
                            {
                                int neighborRow = row + dr;
                                int neighborCol = col + dc;

                                if (neighborRow >= 0 && neighborRow < height &&
                                    neighborCol >= 0 && neighborCol < width)
                                {
                                    if (forest[neighborRow, neighborCol] == 'B')
                                    {
                                        newForest[neighborRow, neighborCol] = 'F';
                                    }
                                }
                            }
                        }
                    }
                }
            }

            forest = newForest;
        }

        static void UpdateBurntTrees()
        {
            char[,] newForest = new char[height, width];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    newForest[row, col] = forest[row, col];
                }
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (forest[row, col] == 'F')
                    {
                        newForest[row, col] = 'f';
                    }
                    else if (forest[row, col] == 'f')
                    {
                        newForest[row, col] = '-';
                    }
                }
            }

            forest = newForest;
        }

        static void GrowNewTrees()
        {
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (forest[row, col] == '-' && random.NextDouble() < growthProbability / 10.0)
                    {
                        forest[row, col] = 'B';
                    }
                }
            }
        }
    }
}
