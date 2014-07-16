using GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimulatorUI.Controllers
{
    public class SimulationController : Controller
    {
       public ActionResult Index()
        {
            ViewBag.sent = MvcApplication.GlobalObserver.c_sent;
            return View();
        }

        public async Task<ActionResult> Start()
        {
            int batch_count = int.Parse(Request.Params["batchcount"]);
            int batch_size = int.Parse(Request.Params["batchsize"]);
            int delay = int.Parse(Request.Params["delay"]);
            int runtime = int.Parse(Request.Params["runtime"]);
            string url = Request.Params["testurl"];

            // create an aggregator grain to track results from the load test
            IAggregatorGrain aggregator = AggregatorGrainFactory.GetGrain(0);

            // set this SimulationController class as an observer on the aggregator grain
            ISimulationObserver observer = await SimulationObserverFactory.CreateObjectReference(MvcApplication.GlobalObserver);  // convert our class into a grain reference
            await aggregator.SetObserver(observer);  // then set ourselves up to receive notifications on ReportResults()

            // Instantiate the manager grains and start the simulations
            // Pause between each batch to ramp up load gradually
            for (int i = 0; i < batch_count; i++)
            {
                Console.WriteLine("Starting batch #{0}", i + 1);
                IManagerGrain manager = ManagerGrainFactory.GetGrain(i);

                await manager.SetAggregator(aggregator); // link in the aggregator
                await manager.StartSimulators(i*delay, batch_size, url);  // start the simulation
            }

            return RedirectToAction("index");
        }
    }
}