using GrainInterfaces;
using Orleans.Host.Azure.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SimulatorUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static SimulationObserver GlobalObserver;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Observer

            GlobalObserver = new SimulationObserver();

            // Orleans Client

            if (!OrleansAzureClient.IsInitialized)
            {
                OrleansAzureClient.Initialize(Server.MapPath(@"~/ClientConfiguration.xml"));
            }
        }
    }
}
