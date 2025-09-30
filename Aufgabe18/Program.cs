using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Deine Eingabe: ");
            string text = Console.ReadLine();

            if (!string.IsNullOrEmpty(text))
            {
                ZähleVokale(text);
            }
            else
            {
                Console.WriteLine("Kein Text eingegeben!");
            }

            Console.WriteLine("\nDrücken Sie eine beliebige Taste zum Beenden...");
            Console.ReadKey();
        }

        static void ZähleVokale(string text)
        {
            char[] vokale = { 'a', 'e', 'i', 'o', 'u', 'ä', 'ö', 'ü', 'A', 'E', 'I', 'O', 'U', 'Ä', 'Ö', 'Ü' };
            Dictionary<char, int> vokalZähler = new Dictionary<char, int>();
            int gesamtVokale = 0;

            foreach (char vokal in vokale)
            {
                vokalZähler[vokal] = 0;
            }

            foreach (char zeichen in text)
            {
                if (vokalZähler.ContainsKey(zeichen))
                {
                    vokalZähler[zeichen]++;
                    gesamtVokale++;
                }
            }

            Console.WriteLine($"\nDein Text hat total {gesamtVokale} Vokale.");

            foreach (var paar in vokalZähler)
            {
                if (paar.Value > 0)
                {
                    Console.WriteLine($"Der Buchstabe '{paar.Key}' kommt {paar.Value} mal vor.");
                }
            }
        }
    }
}
