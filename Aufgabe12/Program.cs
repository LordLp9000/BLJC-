// Ignore Spelling: Aufgabe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe12
{
    internal class Program
    {
        static int[] SumUp(int[] arr)
        {
            int[] result = new int[arr.Length];
            int sum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
                result[i] = sum;
            }

            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("Zahlen Aufsummieren");
            Console.WriteLine("---------------------");
            Console.WriteLine();
            Console.WriteLine("Geben Sie die zu summierenden Ganzzahlen mit Komma getrennt ein:");

            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Bitte geben Sie gültige Zahlen ein.");
                return;
            }

            string[] inputArray = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < inputArray.Length; i++)
            {
                inputArray[i] = inputArray[i].Trim();
            }

            try
            {
                int[] numbers = Array.ConvertAll(inputArray, int.Parse);

                int[] result = SumUp(numbers);

                Console.WriteLine();
                Console.WriteLine("Resultat:");
                for (int i = 0; i < numbers.Length; i++)
                {
                    Console.WriteLine($"[{i}] -> {numbers[i]}, [{i + 1}] -> {result[i]}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ungültige Eingabe. Bitte geben Sie gültige Ganzzahlen getrennt durch Kommas ein.");
            }
        }
    }
}
