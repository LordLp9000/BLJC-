using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable VSSpell001 // Spell Check
namespace Aufgabe16
#pragma warning restore VSSpell001 // Spell Check
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool weiterspielen = true;
            Random random = new Random();

            while (weiterspielen)
            {
                int zufallsZahl = random.Next(1, 101);
                int versuche = 0;
                bool geraten = false;

                Console.WriteLine("Deine Zahl (1...100):");

                while (!geraten)
                {
                    string eingabe = Console.ReadLine();
                    versuche++;

                    if (int.TryParse(eingabe, out int gerateneZahl))
                    {
                        if (gerateneZahl < zufallsZahl)
                        {
                            Console.WriteLine("Zahl ist zu klein! Nächster Versuch:");
                        }
                        else if (gerateneZahl > zufallsZahl)
                        {
                            Console.WriteLine("Zahl ist zu gross! Nächster Versuch:");
                        }
                        else
                        {
                            geraten = true;
                            Console.WriteLine($"Die Zahl stimmt! Du hast total {versuche} Versuche benötigt. Noch einmal spielen? (y/n)");

                            string antwort = Console.ReadLine();
                            if (antwort?.ToLower() != "y")
                            {
                                weiterspielen = false;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bitte geben Sie eine gültige Zahl ein!");
                    }
                }
            }
        }
    }
}
