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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using Orleans;
using GrainInterfaces;

namespace Grains
{
    /// <summary>
    /// Orleans grain implementation class.
    /// </summary>
    public class SimulatorGrain : GrainBase, ISimulatorGrain
    {
        List<HttpWebResponse> _responses = new List<HttpWebResponse>();
        OrleansLogger _logger;
        IManagerGrain _manager;
        IOrleansTimer _reqtimer, _stattimer;
        string _url;

        static int MAX_DELAY = 5; // seconds
        static int PERIOD = 1; // seconds
        static int REPORT_PERIOD = 5; // seconds

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
        public Task StartSimulation(long id, string url, IManagerGrain manager)
        {
            _url = url;
            _manager = manager;

            var rand = new Random();

            _reqtimer = RegisterTimer(SendRequest, null, 
                    TimeSpan.FromSeconds(rand.Next(MAX_DELAY)), TimeSpan.FromSeconds(PERIOD));
            _stattimer = RegisterTimer(ReportResults, null, 
                    TimeSpan.FromSeconds(REPORT_PERIOD), TimeSpan.FromSeconds(REPORT_PERIOD));

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
        public async Task SendRequest(object o)
        {
            try
            {
                // make http request 
                HttpWebRequest req = HttpWebRequest.CreateHttp(_url);
                var resp = await req.GetResponseAsync();
                resp.Close();

                // log the respomse 
                _responses.Add((HttpWebResponse)resp);
            }
            catch (Exception e)
            {
                _logger.Error(0, "Error:", e);
            }
        }

        /// <summary>
        /// Periodically report results to the manager grain.
        /// </summary>
        /// <param name="o"></param>
        public async Task ReportResults(object o)
        {
            var temp = new List<HttpWebResponse>(_responses);
            _responses.Clear();
            await _manager.SendResults(temp);
        }
    }
}
