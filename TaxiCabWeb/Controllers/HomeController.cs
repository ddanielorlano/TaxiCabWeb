using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FareCalculator;

namespace TaxiCabWeb.Controllers
{
    public class HomeController : Controller
    {
        IFareCalculator _fareCalculator;
        public HomeController(IFareCalculator fareCalculator)
        {
            _fareCalculator = fareCalculator;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CalculateRate(Models.TaxiCabRateRequestModel taxiCabRateModel)
        {

            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = taxiCabRateModel.MilesTraveledBelow6mph,
                MinutesTraveledAbove6mph = taxiCabRateModel.MinutesTraveledAbove6pmh,
                RideBeginDateTime = taxiCabRateModel.RideBeginDateTime
            };

            TaxiFareResult taxiResult = (TaxiFareResult)_fareCalculator.CalculateFare(taxiFareRequest);

            Models.TaxiCabRateResponseModel rsp = new Models.TaxiCabRateResponseModel
            {
                EntryFare = taxiResult.EntryFare,
                FareMilesTraveledBelow6mph = taxiResult.FareMilesTraveledBelow6mph,
                FareMinutesTraveledAbove6mph = taxiResult.FareMinutesTraveledAbove6mph,
                NightSurcharge = taxiResult.NightSurcharge,
                NyStateTaxSurcharge = taxiResult.NyStateTaxSurcharge,
                PeakWeakdaySurcharge = taxiResult.PeakWeekdaySurcharge,
                TotalFare = taxiResult.TotalFare
            };

            return Json(rsp, JsonRequestBehavior.AllowGet);
        }
    }
}