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
    public class SimulationController : Controller, ISimulationObserver
    {
        ISimulationObserver observer;
        List<IManagerGrain> managers = new List<IManagerGrain>();

        const int BATCH_COUNT = 1;
        const int BATCH_SIZE = 100;
        const int DELAY_STEPS = 15; // seconds
        const int RUN_TIME = 300; // seconds
        const string URL = "http://localhost:4000/";  // Change to point to the web site under test

        // GET: Simulation
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Start()
        {
            // create an aggregator grain to track results from the load test
            IAggregatorGrain aggregator = AggregatorGrainFactory.GetGrain(0);

            // set this SimulationController class as an observer on the aggregator grain
            observer = await SimulationObserverFactory.CreateObjectReference(this);  // convert our class into a grain reference
            await aggregator.SetObserver(observer);  // then set ourselves up to receive notifications on ReportResults()

            // Instantiate the manager grains and start the simulations
            // Pause between each batch to ramp up load gradually
            for (int i = 0; i < BATCH_COUNT; i++)
            {
                Console.WriteLine("Starting batch #{0}", i + 1);
                IManagerGrain manager = ManagerGrainFactory.GetGrain(i);
                managers.Add(manager);  // store grain reference 

                await manager.SetAggregator(aggregator); // link in the aggregator
                await manager.StartSimulators(i*DELAY_STEPS*1000, BATCH_SIZE, URL);  // start the sinulation
            }

            return RedirectToAction("index");
        }

        public void ReportResults(long millis, int sent, int errors, long size)
        {
            var avg = sent / (millis / 1000);
            Console.WriteLine("avg req/s: {0} sent: {2} errors: {3} size: {4}", avg, millis, sent, errors, size);
        }
    }
}