using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FareCalculator
{
    public class FareResult : IFareResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public decimal TotalFare { get; set; }
    }
}
