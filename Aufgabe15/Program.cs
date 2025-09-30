using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Tannenbaum Zeichner ===");
            Console.WriteLine();

            int stammBreite = GetUserInput("Wie breit soll der Stamm sein? ");
            int stammHoehe = GetUserInput("Wie hoch soll der Stamm sein? ");
            int kronenHoehe = GetUserInput("Wie hoch soll die Krone sein? ");

            Console.WriteLine();
            Console.WriteLine("Hier ist Ihr Tannenbaum:");
            Console.WriteLine();

            ZeichneKrone(kronenHoehe);
            ZeichneStamm(stammBreite, stammHoehe, kronenHoehe);

            Console.WriteLine();
            Console.WriteLine("Drücken Sie eine beliebige Taste zum Beenden...");
            Console.ReadKey();
        }

        static int GetUserInput(string prompt)
        {
            int wert;
            do
            {
                Console.Write(prompt);
                string eingabe = Console.ReadLine();

                if (int.TryParse(eingabe, out wert) && wert > 0)
                {
                    return wert;
                }

                Console.WriteLine("Bitte geben Sie eine gültige positive Zahl ein.");
            } while (true);
        }

        static void ZeichneKrone(int hoehe)
        {
            for (int zeile = 0; zeile < hoehe; zeile++)
            {
                int breite = 1 + 2 * zeile;
                int maxBreite = 1 + 2 * (hoehe - 1);
                int leerzeichen = (maxBreite - breite) / 2;

                for (int j = 0; j < leerzeichen; j++)
                {
                    Console.Write(" ");
                }

                for (int j = 0; j < breite; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }
        }

        static void ZeichneStamm(int breite, int hoehe, int kronenHoehe)
        {
            int maxKronenBreite = 1 + 2 * (kronenHoehe - 1);
            int stammOffset = (maxKronenBreite - breite) / 2;

            for (int i = 0; i < hoehe; i++)
            {
                for (int j = 0; j < stammOffset; j++)
                {
                    Console.Write(" ");
                }

                for (int j = 0; j < breite; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }
        }
    }
}
