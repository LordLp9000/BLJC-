// Ignore Spelling: Aufgabe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Aufgabe14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Prüfen, ob es sich bei einem Jahr um ein Schaltjahr handelt.");
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Console.WriteLine("Eingabe Jahr (q to quit):");
            string eingabe = Console.ReadLine();

            if (eingabe.ToLower() == "q")
            {
                return;
            }

            if (int.TryParse(eingabe, out int jahr))
            {
                if (IsLeapYear(jahr))
                {
                    Console.WriteLine($"{jahr} ist ein Schaltjahr.");
                    Console.WriteLine("Eingabe Jahr (q to quit):");
                    eingabe = Console.ReadLine();
                    Console.ReadKey();

                }
                else
                {
                    Console.WriteLine($"{jahr} ist kein Schaltjahr.");
                    Console.WriteLine("Eingabe Jahr (q to quit):");
                    eingabe = Console.ReadLine();
                    Console.ReadKey();

                }
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine gültige Jahreszahl ein.");
                Console.WriteLine("Eingabe Jahr (q to quit):");
                eingabe = Console.ReadLine();
                Console.ReadKey();
            }
        }

        static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }
    }
}
