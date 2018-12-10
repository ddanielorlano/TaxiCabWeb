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


        public ActionResult CalculateRate(Models.TaxiCabRateModel taxiCabRateModel)
        {
            TaxiCab.TaxiCabFare taxicabFare = new TaxiCab.TaxiCabFare(taxiCabRateModel.RideTime, taxiCabRateModel.MilesTraveledBelow6mph, taxiCabRateModel.MinutesTravelingAbove6pmh);
            return Json(taxicabFare);
        }
    }
}