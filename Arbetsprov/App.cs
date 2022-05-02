using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Media;

namespace Arbetsprov
{
    internal class App
    {
        internal static void Start()
        {
            var sp = PlaySoundLoop();

            string[] data = Utility.FetchData();

            PrintDate(data);

            if (data != null || data.Length > 0)
            {
                PrintAssignment1(data);
                PrintAssignment2(data);
            }

            PressAnyKeyToExit(sp);
        }

        private static void PressAnyKeyToExit(SoundPlayer sp)
        {
            sp.Stop();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\nPress any key to exit application..");
            Console.ResetColor();
            Console.ReadLine();
        }

        private static SoundPlayer PlaySoundLoop()
        {
            SoundPlayer sp = new(@$"{Directory.GetCurrentDirectory()}\ES_Typewriter Type 10 - SFX Producer.wav");
            sp.PlayLooping();
            return sp;
        }

        private static void PrintDate(string[] data)
        {
            PrinterAI AI = new();

            if (data == null || data.Length <= 0)
            {
                AI.WriteLine($"Could not read file from {Directory.GetCurrentDirectory()}");

                return;
            }

            foreach (var item in data)
            {
                AI.WriteLine(item);
            }

            AI.WriteLine("--------------------------------------------------------------------------\n");
        }

        private static void PrintAssignment1(string[] data)
        {
            PrinterAI AI = new();
            AI.WriteLine("Calculating GAV. Assignment 1..\n");
            string requestDate = "20071231";
            var res1 = Calculate.CalculateGAV(data, DateTime.ParseExact(requestDate, "yyyyMMdd", CultureInfo.InvariantCulture));

            foreach (var gav in res1)
            {
                AI.WriteLine($"Paper: {gav.Name}, Amount: {gav.AmountOfPapers}, Acquisition Value: {Math.Round(gav.Value, 2)} SEK");
            }

            AI.WriteLine("--------------------------------------------------------------------------\n");
        }

        private static void PrintAssignment2(string[] data)
        {
            PrinterAI AI = new();
            string requestDate;
            AI.WriteLine("Calculating market value. Assignment 2..\n");
            requestDate = "20080228";
            List<(string, double)> courseValue = new() { ("ABB LTD", 159.50), ("SSAB B", 160.50), ("VOLVO B", 93.75) };
            var res2 = Calculate.CalculateMarketValue(data, DateTime.ParseExact(requestDate, "yyyyMMdd", CultureInfo.InvariantCulture), courseValue);

            foreach (var mv in res2)
            {
                AI.WriteLine($"Paper: {mv.Name}, Amount: {mv.AmountOfPapers}, Market Value: {Math.Round(mv.Value, 2)} SEK");
            }
        }
    }
}
