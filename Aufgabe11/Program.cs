// Ignore Spelling: Aufgabe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Quersumme einer Zahl berechnen");
            Console.WriteLine("Geben Sie die erste Zahl ein:");
            string eingabe1 = Console.ReadLine();
            Console.WriteLine("Geben Sie die zweite Zahl ein:");
            string eingabe2 = Console.ReadLine();

            int startZahl = Convert.ToInt32(eingabe1);
            int endZahl = Convert.ToInt32(eingabe2);


            if (startZahl > endZahl)
            {
                int temp = startZahl;
                startZahl = endZahl;
                endZahl = temp;
            }

            Console.WriteLine();
            Console.WriteLine("Zahl\tQuersumme\tZahl/Quersumme");
            Console.WriteLine("----\t---------\t--------------");

            for (int i = startZahl; i <= endZahl; i++)
            {
                int quersumme = BerechneQuersumme(i);

                if (quersumme > 0 && i % quersumme == 0)
                {
                    int resultat = i / quersumme;
                    Console.WriteLine($"{i}\t{quersumme}\t\t{resultat}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Drücken Sie eine Taste zum Beenden...");
            Console.ReadKey();
        }

        /// <summary>
        /// Berechnet die Quersumme einer Zahl
        /// </summary>
        /// <param name="zahl">Die Zahl, deren Quersumme berechnet werden soll</param>
        /// <returns>Die Quersumme der Zahl</returns>
        static int BerechneQuersumme(int zahl)
        {
            int summe = 0;
            zahl = Math.Abs(zahl);

            while (zahl > 0)
            {
                summe += zahl % 10;
                zahl /= 10;
            }

            return summe;
        }
    }
}
