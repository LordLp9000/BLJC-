using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal class Program
    {
        static char[,] gameState = new char[4, 4];
        static char[,] memory = new char[4, 4];
        static Random random = new Random();
        static int versuche = 0;
        static int gefundeneParare = 0;

        static char[] symbole = { '☺', '#', '♦', '♠', '♥', '♣', '♫', '☼' };

        static void Main(string[] args)
        {
            ZeigeBegruessung();
            InitialisiereSpielzustand();
            InitialisiereMemoryInhalt();

            while (gefundeneParare < 8)
            {
                ZeigeSpielzustand();
                BearbeiteBenutzerEingabe();
            }

            ZeigeSpielzustand();
            Console.WriteLine($"Gratulation. Spiel beendet. Du hast {versuche} Versuche benötigt.");
            Console.ReadKey();
        }

        static void ZeigeBegruessung()
        {
            Console.WriteLine("MEMORY - Finden Sie die passenden Symbole!");
            Console.WriteLine("Geben Sie zwei Positionen in der Form: ZeileSpalteZeileSpalte ein.");
            Console.WriteLine("Zum Aufdecken wählen Sie zwei Positionen in der Form: ZeileSpalteZeileSpalte.");
            Console.WriteLine("Z.B.: 2142 deckt das Symbol in Zeile 2 u. Spalte 1 sowie in Zeile 4 u. Spalte 2.");
            Console.WriteLine();
        }

        static void InitialisiereSpielzustand()
        {
            for (int zeile = 0; zeile < 4; zeile++)
            {
                for (int spalte = 0; spalte < 4; spalte++)
                {
                    gameState[zeile, spalte] = '?';
                }
            }
        }

        static void InitialisiereMemoryInhalt()
        {
            List<char> alleSymbole = new List<char>();

            for (int i = 0; i < symbole.Length; i++)
            {
                alleSymbole.Add(symbole[i]);
                alleSymbole.Add(symbole[i]);
            }

            for (int i = alleSymbole.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = alleSymbole[i];
                alleSymbole[i] = alleSymbole[j];
                alleSymbole[j] = temp;
            }

            int index = 0;
            for (int zeile = 0; zeile < 4; zeile++)
            {
                for (int spalte = 0; spalte < 4; spalte++)
                {
                    memory[zeile, spalte] = alleSymbole[index];
                    index++;
                }
            }
        }

        static void ZeigeSpielzustand()
        {
            Console.Clear();
            Console.WriteLine("MEMORY");
            Console.WriteLine("  1 2 3 4");

            for (int zeile = 0; zeile < 4; zeile++)
            {
                Console.Write($"{zeile + 1} ");
                for (int spalte = 0; spalte < 4; spalte++)
                {
                    Console.Write(gameState[zeile, spalte] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void BearbeiteBenutzerEingabe()
        {
            for (int zeile = 0; zeile < 4; zeile++)
            {
                for (int spalte = 0; spalte < 4; spalte++)
                {
                    if (gameState[zeile, spalte] == 'O')
                    {
                        gameState[zeile, spalte] = '?';
                    }
                }
            }

            Console.Write("Ihre Eingabe: ");
            string eingabe = Console.ReadLine();

            if (string.IsNullOrEmpty(eingabe) || eingabe.Length != 4)
            {
                Console.WriteLine("Ungültige Eingabe! Bitte geben Sie 4 Ziffern ein.");
                Console.ReadKey();
                return;
            }

            if (!int.TryParse(eingabe[0].ToString(), out int zeile1) ||
                !int.TryParse(eingabe[1].ToString(), out int spalte1) ||
                !int.TryParse(eingabe[2].ToString(), out int zeile2) ||
                !int.TryParse(eingabe[3].ToString(), out int spalte2))
            {
                Console.WriteLine("Ungültige Eingabe! Nur Ziffern erlaubt.");
                Console.ReadKey();
                return;
            }

            zeile1--; spalte1--; zeile2--; spalte2--;

            if (zeile1 < 0 || zeile1 > 3 || spalte1 < 0 || spalte1 > 3 ||
                zeile2 < 0 || zeile2 > 3 || spalte2 < 0 || spalte2 > 3)
            {
                Console.WriteLine("Ungültige Eingabe! Positionen müssen zwischen 1 und 4 liegen.");
                Console.ReadKey();
                return;
            }

            if (zeile1 == zeile2 && spalte1 == spalte2)
            {
                Console.WriteLine("Ungültige Eingabe! 2x die gleiche Position.");
                Console.ReadKey();
                return;
            }

            if (gameState[zeile1, spalte1] == ' ' || gameState[zeile2, spalte2] == ' ')
            {
                Console.WriteLine("Ungültiger Versuch! An mindestens einer Position wurde das Symbol bereits aufgedeckt.");
                Console.ReadKey();
                return;
            }

            versuche++;

            gameState[zeile1, spalte1] = memory[zeile1, spalte1];
            gameState[zeile2, spalte2] = memory[zeile2, spalte2];

            ZeigeSpielzustand();

            if (memory[zeile1, spalte1] == memory[zeile2, spalte2])
            {
                Console.WriteLine("Treffer!");
                gameState[zeile1, spalte1] = ' ';
                gameState[zeile2, spalte2] = ' ';
                gefundeneParare++;
            }
            else
            {
                Console.WriteLine("Leider kein Treffer.");
                Console.ReadKey();
                gameState[zeile1, spalte1] = 'O';
                gameState[zeile2, spalte2] = 'O';
            }

            if (gefundeneParare < 8)
            {
                Console.ReadKey();
            }
        }
    }
}
