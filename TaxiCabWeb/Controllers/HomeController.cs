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
            if (!taxiCabRateModel.RideBeginDateTime.HasValue)
                return Json(new Models.TaxiCabRateResponseModel { Success = false }, JsonRequestBehavior.AllowGet);

            TaxiFareRequest taxiFareRequest = new TaxiFareRequest
            {
                MilesTraveledBelow6mph = taxiCabRateModel.MilesTraveledBelow6mph,
                MinutesTraveledAbove6mph = taxiCabRateModel.MinutesTraveledAbove6pmh,
                RideBeginDateTime = taxiCabRateModel.RideBeginDateTime
            };

            TaxiFareResult fareResult = (TaxiFareResult)_fareCalculator.CalculateFare(taxiFareRequest);

            Models.TaxiCabRateResponseModel rsp = new Models.TaxiCabRateResponseModel
            {
                EntryFare = fareResult.EntryFare,
                FareMilesTraveledBelow6mph = fareResult.FareMilesTraveledBelow6mph,
                FareMinutesTraveledAbove6mph = fareResult.FareMinutesTraveledAbove6mph,
                NightSurcharge = fareResult.NightSurcharge,
                NyStateTaxSurcharge = fareResult.NyStateTaxSurcharge,
                PeakWeakdaySurcharge = fareResult.PeakWeekdaySurcharge,
                TotalFare = fareResult.TotalFare,
                Success = fareResult.Success
            };

            return Json(rsp, JsonRequestBehavior.AllowGet);
        }
    }
}