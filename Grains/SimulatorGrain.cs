using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using Orleans;
using GrainInterfaces;
using Orleans.RuntimeCore;

namespace Grains
{
    /// <summary>
    /// Orleans grain implementation class.
    /// </summary>
    public class SimulatorGrain : GrainBase, ISimulatorGrain
    {
        int _sentRequests;
        OrleansLogger _logger;
        IManagerGrain _manager;
        IOrleansTimer _reqtimer, _stattimer;

        /// <summary>
        /// Grain activation.
        /// </summary>
        /// <returns></returns>
        public override Task ActivateAsync()
        {
            _logger = base.GetLogger("Simulator");
            return base.ActivateAsync();
        }

        /// <summary>
        /// Start the simulation.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="manager"></param>
        /// <returns></returns>
        public Task<string> StartSimulation(string name, IManagerGrain manager)
        {
            _manager = manager;

            _reqtimer = RegisterTransientTimer(SendRequest, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(1000));
            _stattimer = RegisterTransientTimer(ReportResults, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(2000));
            
            return Task.FromResult("Started " + name);
        }

        /// <summary>
        /// Stop the simulation by disposing of the timers.
        /// </summary>
        /// <returns></returns>
        public Task StopSimulation()
        {
            _reqtimer.Dispose();
            _stattimer.Dispose();

            return TaskDone.Done;
        }

        /// <summary>
        /// Send an asynchronous request and await the response.
        /// </summary>
        /// <param name="o"></param>
        public async void SendRequest(object o)
        {
            HttpWebRequest req = HttpWebRequest.CreateHttp("http://localhost/");
            using (HttpWebResponse resp = await req.GetResponseAsync() as HttpWebResponse)
            {
                if (resp.StatusCode != HttpStatusCode.OK)
                    _logger.Info("StatusCode={0}", resp.StatusCode);
                ++_sentRequests;
            }            
        }

        /// <summary>
        /// Periodically report results to the manager grain.
        /// </summary>
        /// <param name="o"></param>
        public async void ReportResults(object o)
        {
            await _manager.ReportResults(_sentRequests);
            _sentRequests = 0;
        }
    }
}
