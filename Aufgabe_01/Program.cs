// Ignore Spelling: Aufgabe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Diese Programm Rechnet 2 Zahlen zusammen");
            Console.WriteLine("Geben Sie die erste Zahl ein:");
            string eingabe1 = Console.ReadLine();
            int zahl1 = Convert.ToInt32(eingabe1);
            Console.WriteLine("Geben Sie die zweite Zahl ein:");
            string eingabe2 = Console.ReadLine();
            int zahl2 = Convert.ToInt32(eingabe2);
            int ergebnis = zahl1 + zahl2;
            Console.WriteLine("Das Ergebnis ist: " + ergebnis);
            Console.ReadKey();
        }
    }
}
