//*********************************************************//
//    Copyright (c) Microsoft. All rights reserved.
//    
//    Apache 2.0 License
//    
//    You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//    
//    Unless required by applicable law or agreed to in writing, software 
//    distributed under the License is distributed on an "AS IS" BASIS, 
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
//    implied. See the License for the specific language governing 
//    permissions and limitations under the License.
//
//*********************************************************

using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrainInterfaces;
using System.Diagnostics;
using System.Net;

namespace Grains
{
    public class ManagerGrain : GrainBase, IManagerGrain
    {
        OrleansLogger _logger;
        private List<ISimulatorGrain> _sims = new List<ISimulatorGrain>();
        private IAggregatorGrain _aggregator;
        IOrleansTimer _stattimer, _starttimer;
        int _count;
        string _url;

        // Counters
        int c_total_requests;
        int c_failed_requests;

        static int REPORT_PERIOD = 10; // seconds

        public Task SetAggregator(IAggregatorGrain aggregator)
        {
            _aggregator = aggregator;
            return TaskDone.Done;
        }

        public override Task ActivateAsync()
        {
            _logger = base.GetLogger("Manager");

            return base.ActivateAsync();
        }

        /// <summary>
        /// Instantiate simulator grains and start the simulation on each.
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public async Task StartSimulators(int delay, int count, string url)
        {
            _count = count;
            _url = url;
            _starttimer = RegisterTimer(StartSimulatorsDelayed, null, TimeSpan.FromSeconds(delay), TimeSpan.FromDays(1));
        }

        private async Task StartSimulatorsDelayed(object o)
        {
            List<Task> tasks = new List<Task>();

            // Stop the one-time timer
            _starttimer.Dispose();

            long start = this.GetPrimaryKeyLong() * _count;
            for (long i = start; i < start + _count; i++)
            {
                ISimulatorGrain grainRef = SimulatorGrainFactory.GetGrain(i);
                _sims.Add(grainRef);
                tasks.Add(grainRef.StartSimulation(i, _url, this));
            }

            await Task.WhenAll(tasks);  // wait until all grains have started

            _logger.Info("*** " + _count + " simulators started.");

            _stattimer = RegisterTimer(ReportResults, null, 
                    TimeSpan.FromSeconds(REPORT_PERIOD), TimeSpan.FromSeconds(REPORT_PERIOD));
        }

        /// <summary>
        /// Stop all the simulator grains.
        /// </summary>
        /// <returns></returns>
        public async Task StopSimulators()
        {
            List<Task> tasks = new List<Task>();

            foreach (var i in _sims)
            {
                tasks.Add(i.StopSimulation());
            }

            await Task.WhenAll(tasks);

            _logger.Info(_sims.Count + " simulators stopped.");
        }

        public async Task ReportResults(object o)
        {
            _logger.Info("*** manager {0} report results: total={1} failed={2}", this.GetPrimaryKeyLong(), c_total_requests, c_failed_requests);
            // send the results back to the aggregator grain
            if (_aggregator != null)
                await _aggregator.AggregateResults(this.GetPrimaryKeyLong(), c_total_requests, c_failed_requests);
            // zero out counter
            c_total_requests = c_failed_requests = 0;
        }

        /// <summary>
        /// The simulator grains will call this method to report results.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public Task SendResults(int total_requests, int failed_requests)
        {
            c_total_requests += total_requests;
            c_failed_requests += failed_requests;

            return TaskDone.Done;
        }
    }
}
