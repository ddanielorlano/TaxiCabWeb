using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxiCabWeb;
using TaxiCabWeb.Controllers;
using FareCalculator;
using TaxiCabWeb.Models;

namespace TaxiCabWeb.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Test_CalculateFareSuccess()
        {
            HomeController controller = new HomeController(new TaxiFareCalculator());
            Models.TaxiCabRateRequestModel reqModel = new TaxiCabRateRequestModel
            {
                MilesTraveledBelow6mph = 1,
                MinutesTraveledAbove6pmh = 1,
                RideBeginDateTime = DateTime.Now
            };

            ActionResult result = controller.CalculateFare(reqModel) as ActionResult;
            TaxiCabRateResponseModel rspModel = (TaxiCabRateResponseModel)((JsonResult)result).Data;

            Assert.IsNotNull(result);
            Assert.IsTrue(rspModel.Success);
        }

        [TestMethod]
        public void Test_CalculateFareNotSuccess()
        {
            HomeController controller = new HomeController(new TaxiFareCalculator());
            Models.TaxiCabRateRequestModel reqModel = new TaxiCabRateRequestModel
            {
                MilesTraveledBelow6mph = 1,
                MinutesTraveledAbove6pmh = 1,
                RideBeginDateTime = null
            };

            ActionResult result = controller.CalculateFare(reqModel) as ActionResult;
            TaxiCabRateResponseModel rspModel = (TaxiCabRateResponseModel)((JsonResult)result).Data;

            Assert.IsNotNull(result);
            Assert.IsFalse(rspModel.Success);
        }

    }
}
