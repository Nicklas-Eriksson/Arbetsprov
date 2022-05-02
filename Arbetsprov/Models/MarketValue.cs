using System;

namespace Arbetsprov
{
    public class MarketValue : IEconomicObject
    {
        public string Name { get; set; } = "Unknown";
        public double Value { get; set; } = 00.00;
        public DateTime Date { get; set; }
        public int AmountOfPapers { get; set; } = 0;
    }
}
