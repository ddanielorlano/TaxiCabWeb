using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiCabWeb.Models
{
    public class TaxiCabRateModel
    {
        public int MinutesTravelingAbove6pmh { get; set; }
        public int MilesTraveledBelow6mph { get; set; }
        public DateTime RideDate { get; set; }
        public DateTime RideTime { get; set; }
    }
}