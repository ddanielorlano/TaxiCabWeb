using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiCabWeb.Models
{
    public class TaxiCabRateRequestModel
    {
        public int MinutesTraveledAbove6pmh { get; set; }
        public int MilesTraveledBelow6mph { get; set; }
        public DateTime RideBeginDateTime { get; set; }
    }

    public class TaxiCabRateResponseModel
    {
        public decimal EntryFare { get; set; }
        public decimal NyStateTaxSurcharge { get; set; }
        public decimal FareMilesTraveledBelow6mph { get; set; }
        public decimal FareMinutesTraveledAbove6mph { get; set; }
        public decimal NightSurcharge { get; set; }
        public decimal PeakWeakdaySurcharge { get; set; }
        public decimal TotalFare { get; set; }
    }
}