using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCab
{
    public class TaxiCabFare
    {
        private const decimal _entryFare = 3m;
        private const decimal _unitFare = .35m;
        private const decimal _nightSurcharge = .50m;
        private const decimal _peakWeakdaySurcharge = 1m;
        private const decimal _nyStateTaxSurcharge = .50m;
        private const decimal _unitFareDistance = .2m;

        private const int _peakWeekdaySurchargeStartHour = 6;
        private const int _peakWeekdaySurchargeEndHour = 8;
        private const int _nightSurchargeStartHour = 10;
        private const int _nightSurchargeEndHour = 6;
        private const int unitFareMilesPerHour = 6;

        private DateTime BeginTime { get; set; }
        private DateTime EstimateEndTime { get; set; }

        private decimal MilesTraveledBelow6pmh { get; set; }
        private int MinutesTraveledAbove6mph { get; set; }

        private bool _isPeakWeekday;
        private bool _isNight { get; set; }

        public TaxiCabFare(DateTime beginTime, decimal milesTraveledBelow6mph, int minutesTraveledAbove6mph)
        {
            BeginTime = beginTime;
            MilesTraveledBelow6pmh = milesTraveledBelow6mph;
            MinutesTraveledAbove6mph = minutesTraveledAbove6mph;

            EstimateEndTime = GetEstimatedEndTime();
            _isNight = BeginTime.Hour >= _nightSurchargeStartHour && EstimateEndTime.Hour <= _nightSurchargeEndHour;
            _isPeakWeekday = (BeginTime.Hour <= _peakWeekdaySurchargeStartHour && EstimateEndTime.Hour <= _peakWeekdaySurchargeEndHour)
                && (BeginTime.DayOfWeek != DayOfWeek.Saturday || BeginTime.DayOfWeek != DayOfWeek.Sunday);
        }

        private DateTime GetEstimatedEndTime()
        {
            int minutesTraveledBelow6mph = (int)(MilesTraveledBelow6pmh / 6) * 60;
            return BeginTime.AddMinutes(minutesTraveledBelow6mph);
        }

        public decimal CalculateFare()
        {
            decimal fare = _entryFare;

            //calculate distance below 6mph
            fare += (MilesTraveledBelow6pmh / _unitFareDistance) * _unitFare;
            //calculate time travled above 6pm
            fare += (MinutesTraveledAbove6mph * _unitFare);
            //calculate night surcharge
            if (_isNight)
                fare += _nightSurcharge;
            if (_isPeakWeekday)
                fare += _peakWeakdaySurcharge;
            //add tax
            fare += _nyStateTaxSurcharge;

            return fare;
        }

    }
}
