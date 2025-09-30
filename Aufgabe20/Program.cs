using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aufgabe20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Witz Generator ===");
            Console.WriteLine();

            while (true)
            {
                try
                {
                    ZeigeWitz();

                    Console.Write("Nächsten Witz holen? j/n ");
                    string antwort = Console.ReadLine();

                    if (antwort?.ToLower() != "j")
                    {
                        break;
                    }

                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler beim Laden des Witzes: {ex.Message}");
                    Console.Write("Erneut versuchen? j/n ");
                    string antwort = Console.ReadLine();

                    if (antwort?.ToLower() != "j")
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("Auf Wiedersehen!");
            Console.ReadKey();
        }

        static void ZeigeWitz()
        {
            string apiUrl = "https://witzapi.de/api/joke/";

            Console.WriteLine("Lade Witz...");

            WebRequest request = WebRequest.Create(apiUrl);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            string jsonData = new StreamReader(responseStream).ReadToEnd();

            response.Close();

            string witzText = ExtrahiereWitzText(jsonData);

            if (!string.IsNullOrEmpty(witzText))
            {
                Console.Clear();
                Console.WriteLine("=== Witz Generator ===");
                Console.WriteLine();
                Console.WriteLine(witzText);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Keine Witze erhalten.");
            }
        }

        static string ExtrahiereWitzText(string jsonData)
        {
            try
            {
                JArray array = JArray.Parse(jsonData);

                if (array.Count > 0)
                {
                    JObject joke = (JObject)array[0];
                    return joke["text"]?.ToString();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON-Parsing Fehler: {ex.Message}");
                return ExtrahiereWitzTextFallback(jsonData);
            }
        }

        static string ExtrahiereWitzTextFallback(string jsonData)
        {
            try
            {
                int textStart = jsonData.IndexOf("\"text\":\"");
                if (textStart == -1) return null;

                textStart += 8; // Länge von "\"text\":\""
                int textEnd = jsonData.IndexOf("\",", textStart);
                if (textEnd == -1)
                {
                    // Falls es das letzte Element ist, suche nach "}"
                    textEnd = jsonData.IndexOf("\"}", textStart);
                    if (textEnd == -1) return null;
                }

                string witzText = jsonData.Substring(textStart, textEnd - textStart);

                witzText = witzText.Replace("\\n", "\n");
                witzText = witzText.Replace("\\r", "\r");
                witzText = witzText.Replace("\\\"", "\"");
                witzText = witzText.Replace("\\\\", "\\");

                return witzText;
            }
            catch
            {
                return null;
            }
        }
    }
}
