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

            // Zeichne die Krone
            ZeichneKrone(kronenHoehe);

            // Zeichne den Stamm
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
            int schichtGroesse = Math.Max(2, hoehe / 4);
            int maxBreite = 1;

            while (aktuelleZeile < hoehe)
            {
                int verbleibendeZeilen = hoehe - aktuelleZeile;
                int dieseSchichtHoehe = Math.Min(schichtGroesse, verbleibendeZeilen);

                int startBreite = maxBreite > 1 ? Math.Max(1, maxBreite - 4) : 1;

                for (int i = 0; i < dieseSchichtHoehe; i++)
                {
                    int breite = startBreite + 2 * i;

                    int maxMoeglicheBreite = hoehe * 2 - 1;
                    int leerzeichen = (maxMoeglicheBreite - breite) / 2;

                    for (int j = 0; j < leerzeichen; j++)
                    {
                        Console.Write(" ");
                    }

                    for (int j = 0; j < breite; j++)
                    {
                        Console.Write("*");
                    }

                    Console.WriteLine();
                    aktuelleZeile++;

                    if (breite > maxBreite)
                        maxBreite = breite;
                }

                schichtGroesse++;
            }
        }

        static void ZeichneStamm(int breite, int hoehe, int kronenHoehe)
        {
            int maxMoeglicheBreite = kronenHoehe * 2 - 1;
            int stammOffset = (maxMoeglicheBreite - breite) / 2;

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
