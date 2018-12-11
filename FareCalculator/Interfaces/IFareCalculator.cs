using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FareCalculator
{
    public interface IFareCalculator
    {
        IFareResult CalculateFare(IFareRequest fareRequest);
    }
}
