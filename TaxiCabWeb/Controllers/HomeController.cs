using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaxiCabWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CalculateRate(Models.TaxiCabRateRequestModel taxiCabRateModel)
        {
            TaxiCab.TaxiCabFare taxicabFare = new TaxiCab.TaxiCabFare(taxiCabRateModel.RideBeginDateTime, taxiCabRateModel.MilesTraveledBelow6mph, taxiCabRateModel.MinutesTravelingAbove6pmh);

            TaxiCab.TaxiCabFareResult fareResult = taxicabFare.CalculateFare();

            Models.TaxiCabRateResponseModel rsp = new Models.TaxiCabRateResponseModel
            {
                EntryFare = fareResult.EntryFare,
                FareMilesTraveledBelow6mph = fareResult.FareMilesTraveledBelow6mph,
                FareMinutesTraveledAbove6mph = fareResult.FareMinutesTraveledAbove6mph,
                NightSurcharge = fareResult.NightSurcharge,
                NyStateTaxSurcharge = fareResult.NyStateTaxSurcharge,
                PeakWeakdaySurcharge = fareResult.PeakWeakdaySurcharge,
                TotalFare = fareResult.TotalFare
            };

            return Json(rsp, JsonRequestBehavior.AllowGet);
        }
    }
}