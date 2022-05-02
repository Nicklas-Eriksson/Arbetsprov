using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Arbetsprov
{
    internal static class Utility
    {
        internal static string[] FetchData()
        {
            List<string> data = new();
            var currentDir = Directory.GetCurrentDirectory();
            const string file = @"\Transaktioner[925].dat";
            using (StreamReader sr = new(string.Concat(currentDir, file)))
            {
                while (!sr.EndOfStream)
                {
                    data.Add(sr.ReadLine());
                }
            }

            return data.ToArray();
        }

        internal static string[] GetNameOfPapers(string[] data)
        {
            List<string> papers = new();

            for (int i = 0; i < data.Length; i++)
            {
                string iteration = data[i];
                if (iteration.Any(c => char.IsLetter(c))) //Checks if iteration contains letters.
                {
                    var nameArray = new string(iteration.Where(c => char.IsWhiteSpace(c) || char.IsLetter(c)).ToArray()).Trim();
                    papers.Add(string.Join(" ", nameArray));
                }
            }

            return papers.Distinct().ToArray();
        }

        /// <summary>
        /// Converts a string of data into different values.
        /// (Name of paper, value, date, amount of papers).
        /// </summary>
        /// <param name="v">String to be parsed.</param>
        /// <returns>(Name of paper, value, date, amount of papers)</returns>
        internal static (string, int, DateTime, int) ConvertDataToValues(string v)
        {
            string name = "";
            int value = 0, amount = 0;
            DateTime date = DateTime.MinValue;

            if (v.Any(c => char.IsLetter(c))) //Checks if iteration contains letters.
            {
                var nameArray = new string(v.Where(c => char.IsWhiteSpace(c) || char.IsLetter(c)).ToArray());
                name = string.Join(" ", nameArray);

                string stringNumbers = new(v.Where(Char.IsDigit).ToArray());
                value = Int32.Parse(stringNumbers.Substring(0, 6));
                date = DateTime.ParseExact(stringNumbers.Substring(6, 8), "yyyyMMdd", CultureInfo.InvariantCulture);

                if (v.Contains("-"))
                {
                    amount = Int32.Parse(stringNumbers.Substring(14, 3)) * -1;
                }
                else
                {
                    amount = Int32.Parse(stringNumbers.Substring(14, 4));
                }
            }

            return (name, value, date, amount);
        }                
    }

    public class PrinterAI : TextWriter
    {
        public override Encoding Encoding => Encoding.ASCII;

        public override void WriteLine(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(input[i]);
                System.Threading.Thread.Sleep(5);
            }
            Console.WriteLine();
        }
    }
}
