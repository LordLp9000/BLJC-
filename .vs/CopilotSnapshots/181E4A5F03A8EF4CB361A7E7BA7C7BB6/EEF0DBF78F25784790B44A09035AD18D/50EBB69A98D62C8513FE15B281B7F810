using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Make your calculation (or press Q to quit): ");
                string eingabe = Console.ReadLine();

                if (string.IsNullOrEmpty(eingabe))
                {
                    Console.WriteLine("Keine Eingabe erhalten!");
                    continue;
                }

                if (eingabe.ToUpper() == "Q")
                {
                    break;
                }

                BerechneAusdruck(eingabe);
            }

            Console.WriteLine("Auf Wiedersehen!");
        }

        static void BerechneAusdruck(string eingabe)
        {
            try
            {
                // Entferne alle Leerzeichen
                string bereinigteEingabe = eingabe.Replace(" ", "");

                if (string.IsNullOrEmpty(bereinigteEingabe))
                {
                    Console.WriteLine("Ungültige Eingabe!");
                    return;
                }

                // Finde den Operator
                char[] operatoren = { '+', '-', '*', '/' };
                int operatorPosition = -1;
                char gefundenerOperator = ' ';

                // Suche nach Operator (nicht am Anfang für negative Zahlen)
                for (int i = 1; i < bereinigteEingabe.Length; i++)
                {
                    if (operatoren.Contains(bereinigteEingabe[i]))
                    {
                        operatorPosition = i;
                        gefundenerOperator = bereinigteEingabe[i];
                        break;
                    }
                }

                if (operatorPosition == -1)
                {
                    Console.WriteLine("Kein gültiger Operator gefunden!");
                    return;
                }

                // Teile die Eingabe in Zahlen auf
                string zahl1String = bereinigteEingabe.Substring(0, operatorPosition);
                string zahl2String = bereinigteEingabe.Substring(operatorPosition + 1);

                if (string.IsNullOrEmpty(zahl1String) || string.IsNullOrEmpty(zahl2String))
                {
                    Console.WriteLine("Ungültige Zahlen!");
                    return;
                }

                // Konvertiere zu Zahlen
                if (!double.TryParse(zahl1String, out double zahl1) || 
                    !double.TryParse(zahl2String, out double zahl2))
                {
                    Console.WriteLine("Ungültige Zahlen eingegeben!");
                    return;
                }

                // Führe die Berechnung durch
                double ergebnis = 0;
                bool gültigeOperation = true;

                switch (gefundenerOperator)
                {
                    case '+':
                        ergebnis = zahl1 + zahl2;
                        break;
                    case '-':
                        ergebnis = zahl1 - zahl2;
                        break;
                    case '*':
                        ergebnis = zahl1 * zahl2;
                        break;
                    case '/':
                        if (zahl2 == 0)
                        {
                            Console.WriteLine("Division durch Null ist nicht erlaubt!");
                            return;
                        }
                        ergebnis = zahl1 / zahl2;
                        break;
                    default:
                        gültigeOperation = false;
                        break;
                }

                if (gültigeOperation)
                {
                    // Zeige das Ergebnis an (formatiert wie in der Referenz)
                    if (ergebnis == Math.Floor(ergebnis))
                    {
                        Console.WriteLine((int)ergebnis);
                    }
                    else
                    {
                        Console.WriteLine(ergebnis);
                    }
                }
                else
                {
                    Console.WriteLine("Ungültiger Operator!");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Fehler bei der Berechnung!");
            }
        }
    }
}
