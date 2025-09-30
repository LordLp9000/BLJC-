// Ignore Spelling: Aufgabe

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

            // Benutzereingaben
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
            // Erstelle mehrere Schichten für den Tannenbaum-Effekt
            int schichtGroesse = Math.Max(2, hoehe / 4); // Jede Schicht hat mindestens 2 Zeilen
            int aktuelleZeile = 0;
            int maxBreite = 1;

            while (aktuelleZeile < hoehe)
            {
                // Berechne wie viele Zeilen für diese Schicht übrig sind
                int verbleibendeZeilen = hoehe - aktuelleZeile;
                int dieseSchichtHoehe = Math.Min(schichtGroesse, verbleibendeZeilen);
                
                // Startbreite für diese Schicht (überlappend mit vorheriger)
                int startBreite = maxBreite > 1 ? Math.Max(1, maxBreite - 4) : 1;
                
                for (int i = 0; i < dieseSchichtHoehe; i++)
                {
                    int breite = startBreite + 2 * i;
                    
                    // Zentrierung basierend auf der breitesten möglichen Zeile
                    int maxMoeglicheBreite = hoehe * 2 - 1;
                    int leerzeichen = (maxMoeglicheBreite - breite) / 2;

                    // Zeichne die Leerzeichen
                    for (int j = 0; j < leerzeichen; j++)
                    {
                        Console.Write(" ");
                    }

                    // Zeichne die Sterne
                    for (int j = 0; j < breite; j++)
                    {
                        Console.Write("*");
                    }

                    Console.WriteLine();
                    aktuelleZeile++;
                    
                    if (breite > maxBreite)
                        maxBreite = breite;
                }
                
                // Erhöhe die Schichtgröße für nachfolgende Schichten
                schichtGroesse++;
            }
        }

        static void ZeichneStamm(int breite, int hoehe, int kronenHoehe)
        {
            // Zentrierung basierend auf der breitesten möglichen Krone
            int maxMoeglicheBreite = kronenHoehe * 2 - 1;
            int stammOffset = (maxMoeglicheBreite - breite) / 2;

            for (int i = 0; i < hoehe; i++)
            {
                // Zeichne die Leerzeichen für die Zentrierung
                for (int j = 0; j < stammOffset; j++)
                {
                    Console.Write(" ");
                }

                // Zeichne den Stamm
                for (int j = 0; j < breite; j++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }
        }
    }
}
