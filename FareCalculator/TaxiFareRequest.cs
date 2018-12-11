using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FareCalculator
{
    public class TaxiFareRequest : IFareRequest
    {
        public int MinutesTraveledAbove6mph { get; set; }
        public int MilesTraveledBelow6mph { get; set; }
        public DateTime? RideBeginDateTime { get; set; }
    }
}
