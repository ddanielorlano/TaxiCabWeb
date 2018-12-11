using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FareCalculator
{
    public class TaxiFareCalculator : IFareCalculator
    {
        private const decimal _entryFare = 3m;
        private const decimal _unitFare = .35m;
        private const decimal _nightSurcharge = .50m;
        private const decimal _peakWeakdaySurcharge = 1m;
        private const decimal _nyStateTaxSurcharge = .50m;
        private const decimal _unitFareDistance = .2m;

        private const int _peakWeekdaySurchargeStartHour = 16;
        private const int _peakWeekdaySurchargeEndHour = 20;
        private const int _nightSurchargeStartHour = 20;
        private const int _nightSurchargeEndHour = 6;
        private readonly TimeSpan _midnightTimeSpan = new TimeSpan(0, 0, 0);
        private readonly TimeSpan _middayTimeSpan = new TimeSpan(12, 0, 0);
        public TaxiFareCalculator() { }

        public IFareResult CalculateFare(IFareRequest fareRequest)
        {
            TaxiFareRequest taxiRequest = fareRequest as TaxiFareRequest;
            if (taxiRequest == null) return new TaxiFareResult { Success = false };
            if (!taxiRequest.RideBeginDateTime.HasValue) return new TaxiFareResult { Success = false, Message = "RideBeginDateTime is null" };

            TaxiFareResult result = new TaxiFareResult
            {
                EntryFare = _entryFare,
                FareMilesTraveledBelow6mph = (taxiRequest.MilesTraveledBelow6mph / _unitFareDistance) * _unitFare,
                FareMinutesTraveledAbove6mph = (taxiRequest.MinutesTraveledAbove6mph * _unitFare),
                NightSurcharge = IsNight(taxiRequest.RideBeginDateTime.Value) ? _nightSurcharge : 0,
                PeakWeekdaySurcharge = IsPeakWeekday(taxiRequest.RideBeginDateTime.Value) ? _peakWeakdaySurcharge : 0,
                NyStateTaxSurcharge = _nyStateTaxSurcharge,
                Success = true
            };

            result.TotalFare = result.EntryFare + result.FareMilesTraveledBelow6mph + result.FareMinutesTraveledAbove6mph + result.NightSurcharge + result.PeakWeekdaySurcharge + result.NyStateTaxSurcharge;

            return result;
        }

        private bool IsNight(DateTime rideBeginDateTime)
        {
            bool isEvening = rideBeginDateTime.TimeOfDay > _middayTimeSpan;

            bool nightSurcharge = isEvening ? rideBeginDateTime.Hour >= _nightSurchargeStartHour : rideBeginDateTime.Hour < _nightSurchargeEndHour;

            return nightSurcharge;
        }

        private bool IsPeakWeekday(DateTime rideBeginDateTime)
        {
            return (rideBeginDateTime.DayOfWeek != DayOfWeek.Saturday && rideBeginDateTime.DayOfWeek != DayOfWeek.Sunday)
                && (rideBeginDateTime.Hour >= _peakWeekdaySurchargeStartHour && rideBeginDateTime.Hour < _peakWeekdaySurchargeEndHour);
        }
    }
}
