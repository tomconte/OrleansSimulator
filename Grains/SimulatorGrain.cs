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

        static int MAX_DELAY = 5; // seconds
        static int PERIOD = 1; // seconds
        static int REPORT_PERIOD = 5; // seconds

        string _connectionString = "Endpoint=sb://telemetry.servicebus.windows.net/;SharedSecretIssuer=owner;SharedSecretValue=sb6Xokb9i6MyZfPAF/n7N3LVUHJwaFRdXuSD6SWksWY=;TransportType=Amqp";

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
        public Task StartSimulation(int id, IManagerGrain manager)
        {
            _manager = manager;

            var rand = new Random();

            _reqtimer = RegisterTransientTimer(SendRequest, null, TimeSpan.FromSeconds(rand.Next(MAX_DELAY)), TimeSpan.FromSeconds(PERIOD));
            _stattimer = RegisterTransientTimer(ReportResults, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(REPORT_PERIOD));

            _logger.Info("Started {0}", id);

            return TaskDone.Done;
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
            try
            {
                HttpWebRequest req = HttpWebRequest.CreateHttp("http://arriis.cloudapp.net/");
                using (HttpWebResponse resp = await req.GetResponseAsync() as HttpWebResponse)
                {
                    if (resp.StatusCode != HttpStatusCode.OK)
                        _logger.Info("StatusCode={0}", resp.StatusCode);
                    // TODO: log details about request result (response code, content length...)
                    ++_sentRequests;
                }       
            }
            catch (Exception e)
            {
                _logger.Error("Error: {0}", e);
            }
        }

        /// <summary>
        /// Periodically report results to the manager grain.
        /// </summary>
        /// <param name="o"></param>
        public async void ReportResults(object o)
        {
            // TODO: report request details
            await _manager.ReportResults(_sentRequests);
            _sentRequests = 0;
        }
    }
}
