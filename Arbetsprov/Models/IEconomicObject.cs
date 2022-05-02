using System;

namespace Arbetsprov
{
    public interface IEconomicObject
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public int AmountOfPapers { get; set; }
    }
}
