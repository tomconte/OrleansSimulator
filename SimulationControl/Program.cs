using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using GrainInterfaces;

namespace SimulationControl
{
    public class SimulationController : ISimulationObserver
    {
        ISimulationObserver _observer;

        /// <summary>
        /// Start the simulation via the controller grain.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            OrleansClient.Initialize();

            // Instantiate the manager grain
            IManagerGrain manager = ManagerGrainFactory.GetGrain(0);

            // Get a reference to the observer
            _observer = await SimulationObserverFactory.CreateObjectReference(this);

            // Start the simulation
            await manager.StartSimulators(_observer);
        }

        /// <summary>
        /// This method is called by the manager grain to report aggregated results.
        /// </summary>
        /// <param name="result"></param>
        public void ReportResults(int result)
        {
            Console.WriteLine("Avg req/s: {0}", result);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var prog = new SimulationController();
            prog.Run().Wait();

            Console.WriteLine("--> Press any key to exit program <--");
            Console.ReadKey();

            Environment.Exit(0);
        }
    }
}
