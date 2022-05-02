using System;
using System.Collections.Generic;
using System.Linq;

namespace Arbetsprov
{
    internal static class Calculate
    {
        internal static List<AcquisitionValue> CalculateGAV(string[] data, DateTime toDate)
        {
            List<AcquisitionValue> gavs = new();
            var papers = Utility.GetNameOfPapers(data);

            List<AcquisitionValue> acquisitions = GetListOfAcqusitions(data);

            for (int i = 0; i < papers.Length; i++)
            {
                var relevantAcqusitions = acquisitions.FindAll(a => a.Date <= toDate && a.Name == papers[i]);

                AcquisitionValue av = new()
                {
                    Name = papers[i],
                    Value = relevantAcqusitions.Sum(a => a.Value) / relevantAcqusitions.FindAll(a => a.Value != 0).Count,
                    Date = toDate,
                    AmountOfPapers = relevantAcqusitions.Sum(a => a.AmountOfPapers)
                };

                gavs.Add(av);
            }

            return gavs;
        }

        private static List<AcquisitionValue> GetListOfAcqusitions(string[] data)
        {
            List<AcquisitionValue> acuisitions = new();

            for (int i = 0; i < data.Length; i++)
            {
                // (Name, total value, date, amount)
                (string, double, DateTime, int) res = Utility.ConvertDataToValues(data[i]);
                int amountOfPapers = res.Item4;
                AcquisitionValue gav = new();

                if (!data[i].Contains("-"))
                {
                    gav.Value = (double)res.Item2 / amountOfPapers;
                }
                gav.AmountOfPapers = amountOfPapers;
                gav.Name = res.Item1.Trim();
                gav.Date = res.Item3;
                acuisitions.Add(gav);
            }

            return acuisitions;
        }

        internal static List<MarketValue> CalculateMarketValue(string[] data, DateTime toDate, List<(string, double)> referenceValue)
        {
            List<MarketValue> marketValues = new();
            var papers = Utility.GetNameOfPapers(data);

            List<MarketValue> values = GetListOfMarketValues(data);

            for (int i = 0; i < papers.Length; i++)
            {
                var relevantMarketValues = values.FindAll(a => a.Date <= toDate && a.Name == papers[i]);
                var relevantReferenceValue = referenceValue.Find(v=> v.Item1 == papers[i]).Item2;
                MarketValue av = new()
                {
                    Name = papers[i],
                    Value = relevantReferenceValue * relevantMarketValues.Sum(a => a.AmountOfPapers),
                    Date = toDate,
                    AmountOfPapers = relevantMarketValues.Sum(a => a.AmountOfPapers)
                };

                marketValues.Add(av);
            }

            return marketValues;
        }

        private static List<MarketValue> GetListOfMarketValues(string[] data)
        {
            List<MarketValue> marketValues = new();

            for (int i = 0; i < data.Length; i++)
            {
                // (Name, total value, date, amount)
                (string, double, DateTime, int) res = Utility.ConvertDataToValues(data[i]);
                int amountOfPapers = res.Item4;
                MarketValue mv = new();

                if (!data[i].Contains("-"))
                {
                    mv.Value = (double)res.Item2 / amountOfPapers;
                }
                mv.AmountOfPapers = amountOfPapers;
                mv.Name = res.Item1.Trim();
                mv.Date = res.Item3;
                marketValues.Add(mv);
            }

            return marketValues;
        }
    }
}
