using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FareCalculator
{
    public class TaxiFareCalculator : IFareCalculator
    {
        #region consts
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
        private readonly TimeSpan _middayTimeSpan = new TimeSpan(12, 0, 0);
        #endregion

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
                NightSurcharge = IsNightSurcharge(taxiRequest.RideBeginDateTime.Value) ? _nightSurcharge : 0,
                PeakWeekdaySurcharge = IsPeakWeekday(taxiRequest.RideBeginDateTime.Value) ? _peakWeakdaySurcharge : 0,
                NyStateTaxSurcharge = _nyStateTaxSurcharge,
                Success = true
            };

            result.TotalFare = result.EntryFare + result.FareMilesTraveledBelow6mph + result.FareMinutesTraveledAbove6mph + result.NightSurcharge + result.PeakWeekdaySurcharge + result.NyStateTaxSurcharge;

            return result;
        }

        #region methods
        /// <summary>
        /// Determine wether to add the night surcharge.
        /// </summary>
        /// <returns>
        /// True if evening (12:00PM - 12:00AM) and the ride began after Night Surcharge, else false.
        /// True if not evenining (12:00AM - 12:00PM) and the ride began befor the end of Night Surcharge, else false.
        /// </returns>

        private bool IsNightSurcharge(DateTime rideBeginDateTime)
        {
            bool isEvening = rideBeginDateTime.TimeOfDay > _middayTimeSpan;
            return isEvening ? rideBeginDateTime.Hour >= _nightSurchargeStartHour : rideBeginDateTime.Hour < _nightSurchargeEndHour;
        }

        /// <summary>
        /// Determine wether to add the peak weekday surcharge       
        /// </summary>
        /// <returns>True if it is a weekday and within the peak weekday start and end window. False if it is not a weekday, or not within the peek surcharge window</returns>
        private bool IsPeakWeekday(DateTime rideBeginDateTime)
        {
            return (rideBeginDateTime.DayOfWeek != DayOfWeek.Saturday && rideBeginDateTime.DayOfWeek != DayOfWeek.Sunday)
                && (rideBeginDateTime.Hour >= _peakWeekdaySurchargeStartHour && rideBeginDateTime.Hour < _peakWeekdaySurchargeEndHour);
        }
        #endregion
    }
}
