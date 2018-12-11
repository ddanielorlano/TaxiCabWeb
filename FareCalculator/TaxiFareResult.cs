using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FareCalculator
{
    public class TaxiFareResult : FareResult
    {
        public decimal EntryFare { get; set; }
        public decimal NyStateTaxSurcharge { get; set; }
        public decimal FareMilesTraveledBelow6mph { get; set; }
        public decimal FareMinutesTraveledAbove6mph { get; set; }
        public decimal NightSurcharge { get; set; }
        public decimal PeakWeekdaySurcharge { get; set; }
    }
}
