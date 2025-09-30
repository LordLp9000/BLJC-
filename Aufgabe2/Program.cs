// Ignore Spelling: Aufgabe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Anzahl sekunden im Monat abhängig von Anzahl tagen");
            Console.WriteLine("Geben Sie die Anzahl der Tage ein:");
            string eingabe = Console.ReadLine();
            if (!int.TryParse(eingabe, out int tage) || tage < 28 || tage > 31)
            {
                Console.WriteLine("Eingabe ungültig. Bitte eine Zahl eingeben.");
                return;
            }
            ///Summary
            /// Diese Methode berechnet die Anzahl der Sekunden in einem Monat basierend auf der Anzahl der Tage.   
            ///Summary

            int sekunden = tage * 24 * 60 * 60;
            Console.WriteLine("Die Anzahl der Sekunden im Monat beträgt: " + sekunden);
            Console.ReadKey();

        }
    }
}
