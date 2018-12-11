using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using FareCalculator;
using TaxiCabWeb.Controllers;

namespace TaxiCabWeb
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IFareCalculator, TaxiFareCalculator>();
            container.RegisterType<IController, HomeController>("Home");
            return container;
        }
    }
}