using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FareCalculator.Tests
{
    [TestClass]
    public class TaxiFareCalculatorTests
    {
        [TestMethod]
        public void Test_TotalFareIncludesPeakWeekdaySurcharge()
        {
            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = 2,
                MinutesTraveledAbove6mph = 5,
                RideBeginDateTime = new DateTime(2010, 10, 8, 17, 30, 0)
            };

            TaxiFareCalculator taxiCalculator = new TaxiFareCalculator();
            TaxiFareResult taxiResult = (TaxiFareResult)taxiCalculator.CalculateFare(taxiFareRequest);

            Assert.IsTrue(taxiResult.Success);
            Assert.AreEqual(9.75m, taxiResult.TotalFare);
            Assert.AreEqual(3.50m, taxiResult.FareMilesTraveledBelow6mph);
            Assert.AreEqual(1.75m, taxiResult.FareMinutesTraveledAbove6mph);
            Assert.AreEqual(1m, taxiResult.PeakWeekdaySurcharge);

        }

        [TestMethod]
        public void Test_TotalFareDoesNotIncludesPeakWeekdaySurcharge()
        {
            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = 2,
                MinutesTraveledAbove6mph = 5,
                RideBeginDateTime = new DateTime(2010, 10, 8, 15, 30, 0)
            };

            TaxiFareCalculator taxiCalculator = new TaxiFareCalculator();
            TaxiFareResult taxiResult = (TaxiFareResult)taxiCalculator.CalculateFare(taxiFareRequest);

            Assert.IsTrue(taxiResult.Success);
            Assert.AreEqual(8.75m, taxiResult.TotalFare);
            Assert.AreEqual(3.50m, taxiResult.FareMilesTraveledBelow6mph);
            Assert.AreEqual(1.75m, taxiResult.FareMinutesTraveledAbove6mph);
            Assert.AreEqual(0m, taxiResult.PeakWeekdaySurcharge);

        }

        [TestMethod]
        public void Test_TotalFareDoesNotIncludesPeakWeekdayOnWeekend()
        {
            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = 2,
                MinutesTraveledAbove6mph = 5,
                RideBeginDateTime = new DateTime(2010, 10, 9, 17, 30, 0)
            };

            TaxiFareCalculator taxiCalculator = new TaxiFareCalculator();
            TaxiFareResult taxiResult = (TaxiFareResult)taxiCalculator.CalculateFare(taxiFareRequest);

            Assert.IsTrue(taxiResult.Success);
            Assert.AreEqual(8.75m, taxiResult.TotalFare);
            Assert.AreEqual(3.50m, taxiResult.FareMilesTraveledBelow6mph);
            Assert.AreEqual(1.75m, taxiResult.FareMinutesTraveledAbove6mph);
            Assert.AreEqual(0m, taxiResult.PeakWeekdaySurcharge);
        }

        [TestMethod]
        public void Test_TotalFareIncludesNightSurchargeOnSaturday()
        {
            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = 2,
                MinutesTraveledAbove6mph = 5,
                RideBeginDateTime = new DateTime(2010, 10, 9, 20, 30, 0)
            };

            Assert.AreEqual(DayOfWeek.Saturday, taxiFareRequest.RideBeginDateTime.Value.DayOfWeek);

            TaxiFareCalculator taxiCalculator = new TaxiFareCalculator();
            TaxiFareResult taxiResult = (TaxiFareResult)taxiCalculator.CalculateFare(taxiFareRequest);

            Assert.IsTrue(taxiResult.Success);
            Assert.AreEqual(9.25m, taxiResult.TotalFare);
            Assert.AreEqual(3.50m, taxiResult.FareMilesTraveledBelow6mph);
            Assert.AreEqual(1.75m, taxiResult.FareMinutesTraveledAbove6mph);
            Assert.AreEqual(.5m, taxiResult.NightSurcharge);
            Assert.AreEqual(0m, taxiResult.PeakWeekdaySurcharge);

        }

        [TestMethod]
        public void Test_WeekdayNightSurchargeAndNoPeak()
        {
            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = 2,
                MinutesTraveledAbove6mph = 5,
                RideBeginDateTime = new DateTime(2010, 10, 8, 20, 30, 0)
            };

            Assert.AreEqual(DayOfWeek.Friday, taxiFareRequest.RideBeginDateTime.Value.DayOfWeek);

            TaxiFareCalculator taxiCalculator = new TaxiFareCalculator();
            TaxiFareResult taxiResult = (TaxiFareResult)taxiCalculator.CalculateFare(taxiFareRequest);

            Assert.IsTrue(taxiResult.Success);
            Assert.AreEqual(9.25m, taxiResult.TotalFare);
            Assert.AreEqual(3.50m, taxiResult.FareMilesTraveledBelow6mph);
            Assert.AreEqual(1.75m, taxiResult.FareMinutesTraveledAbove6mph);
            Assert.AreEqual(.5m, taxiResult.NightSurcharge);
            Assert.AreEqual(0m, taxiResult.PeakWeekdaySurcharge);

        }


        [TestMethod]
        public void Test_WeekdayNightSurchargeAt545AM()
        {
            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = 2,
                MinutesTraveledAbove6mph = 5,
                RideBeginDateTime = new DateTime(2010, 10, 8, 5, 45, 0)
            };

            Assert.AreEqual(DayOfWeek.Friday, taxiFareRequest.RideBeginDateTime.Value.DayOfWeek);

            TaxiFareCalculator taxiCalculator = new TaxiFareCalculator();
            TaxiFareResult taxiResult = (TaxiFareResult)taxiCalculator.CalculateFare(taxiFareRequest);

            Assert.IsTrue(taxiResult.Success);
            Assert.AreEqual(9.25m, taxiResult.TotalFare);
            Assert.AreEqual(3.50m, taxiResult.FareMilesTraveledBelow6mph);
            Assert.AreEqual(1.75m, taxiResult.FareMinutesTraveledAbove6mph);
            Assert.AreEqual(.5m, taxiResult.NightSurcharge);
            Assert.AreEqual(0m, taxiResult.PeakWeekdaySurcharge);

        }

        [TestMethod]
        public void Test_WeekdayNightSurchargeAt830PM()
        {
            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = 2,
                MinutesTraveledAbove6mph = 5,
                RideBeginDateTime = new DateTime(2010, 10, 8, 20, 45, 0)
            };

            Assert.AreEqual(DayOfWeek.Friday, taxiFareRequest.RideBeginDateTime.Value.DayOfWeek);

            TaxiFareCalculator taxiCalculator = new TaxiFareCalculator();
            TaxiFareResult taxiResult = (TaxiFareResult)taxiCalculator.CalculateFare(taxiFareRequest);

            Assert.IsTrue(taxiResult.Success);
            Assert.AreEqual(9.25m, taxiResult.TotalFare);
            Assert.AreEqual(3.50m, taxiResult.FareMilesTraveledBelow6mph);
            Assert.AreEqual(1.75m, taxiResult.FareMinutesTraveledAbove6mph);
            Assert.AreEqual(.5m, taxiResult.NightSurcharge);
            Assert.AreEqual(0m, taxiResult.PeakWeekdaySurcharge);

        }

        [TestMethod]
        public void Test_WeekendNightSurchargeBeforeNight745PM()
        {
            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = 2,
                MinutesTraveledAbove6mph = 5,
                RideBeginDateTime = new DateTime(2010, 10, 9, 19, 45, 0)
            };

            Assert.AreEqual(DayOfWeek.Saturday, taxiFareRequest.RideBeginDateTime.Value.DayOfWeek);

            TaxiFareCalculator taxiCalculator = new TaxiFareCalculator();
            TaxiFareResult taxiResult = (TaxiFareResult)taxiCalculator.CalculateFare(taxiFareRequest);

            Assert.IsTrue(taxiResult.Success);
            Assert.AreEqual(8.75m, taxiResult.TotalFare);
            Assert.AreEqual(3.50m, taxiResult.FareMilesTraveledBelow6mph);
            Assert.AreEqual(1.75m, taxiResult.FareMinutesTraveledAbove6mph);
            Assert.AreEqual(0, taxiResult.NightSurcharge);
            Assert.AreEqual(0m, taxiResult.PeakWeekdaySurcharge);

        }

        [TestMethod]
        public void Test_PassMissingValue()
        {
            IFareRequest i;
            TaxiFareCalculator taxiCalculator = new TaxiFareCalculator();
            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = 2,
                MinutesTraveledAbove6mph = 5,
                RideBeginDateTime = null
            };

            TaxiFareResult result = (TaxiFareResult) taxiCalculator.CalculateFare(taxiFareRequest);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }
    }
}
