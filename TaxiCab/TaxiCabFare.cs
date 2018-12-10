using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCab
{
    public class TaxiCabFareResult
    {
        public decimal EntryFare { get; set; }
        public decimal NyStateTaxSurcharge { get; set; }
        public decimal FareMilesTraveledBelow6mph { get; set; }
        public decimal FareMinutesTraveledAbove6mph { get; set; }
        public decimal NightSurcharge { get; set; }
        public decimal PeakWeakdaySurcharge { get; set; }
        public decimal TotalFare { get; set; }

    }

    public class TaxiCabFare
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
        private const int unitFareMilesPerHour = 6;

        private DateTime RideBeginDateTime { get; set; }
        private DateTime EstimateRideEndDateTime { get; set; }

        private decimal MilesTraveledBelow6pmh { get; set; }
        private int MinutesTraveledAbove6mph { get; set; }

        private bool _isPeakWeekday;
        private bool _isNight { get; set; }

        public TaxiCabFare(DateTime rideBeginDateTime, decimal milesTraveledBelow6mph, int minutesTraveledAbove6mph)
        {
            RideBeginDateTime = rideBeginDateTime;
            MilesTraveledBelow6pmh = milesTraveledBelow6mph;
            MinutesTraveledAbove6mph = minutesTraveledAbove6mph;

            EstimateRideEndDateTime = GetEstimatedEndTime();
            _isNight = RideBeginDateTime.Hour >= _nightSurchargeStartHour && EstimateRideEndDateTime.Hour <= _nightSurchargeEndHour;
            _isPeakWeekday = (RideBeginDateTime.Hour >= _peakWeekdaySurchargeStartHour && EstimateRideEndDateTime.Hour <= _peakWeekdaySurchargeEndHour)
                && (RideBeginDateTime.DayOfWeek != DayOfWeek.Saturday || RideBeginDateTime.DayOfWeek != DayOfWeek.Sunday);
        }

        private DateTime GetEstimatedEndTime()
        {
            int minutesTraveledBelow6mph = (int)(MilesTraveledBelow6pmh / 6) * 60;
            return RideBeginDateTime.AddMinutes(minutesTraveledBelow6mph);
        }

        public TaxiCabFareResult CalculateFare()
        {
            var result = new TaxiCabFareResult
            {
                EntryFare = _entryFare,
                FareMilesTraveledBelow6mph = (MilesTraveledBelow6pmh / _unitFareDistance) * _unitFare,
                FareMinutesTraveledAbove6mph = (MinutesTraveledAbove6mph * _unitFare),
                NightSurcharge = _isNight ? _nightSurcharge : 0,
                PeakWeakdaySurcharge = _isPeakWeekday ? _peakWeakdaySurcharge : 0,
                NyStateTaxSurcharge = _nyStateTaxSurcharge
            };

            result.TotalFare = (result.EntryFare + result.FareMilesTraveledBelow6mph + result.FareMinutesTraveledAbove6mph + result.NightSurcharge + result.PeakWeakdaySurcharge + result.NyStateTaxSurcharge);

            return result;
        }

    }
}
