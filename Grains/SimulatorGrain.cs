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
using GPSTracker.Common;
using System.IO;

namespace Grains
{
    /// <summary>
    /// Orleans grain implementation class.
    /// </summary>
    public class SimulatorGrain : GrainBase, ISimulatorGrain
    {
        OrleansLogger _logger;
        IManagerGrain _manager;
        IOrleansTimer _reqtimer, _stattimer;
        string _url;

        // State
        double cur_lat = 0, cur_long = 0;
        Guid device_id;
        double lat_speed, long_speed;
        double speed_factor = 0.25;

        // Counters
        int c_total_requests;
        int c_failed_requests;

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

            Random rand = new Random((int)this.GetPrimaryKeyLong());

            cur_lat = (rand.NextDouble() - 0.5) * 10.0;
            cur_long = (rand.NextDouble() - 0.5) * 10.0;
            device_id = Guid.NewGuid();
            lat_speed = rand.NextDouble() - 0.5;
            long_speed = rand.NextDouble() - 0.5;

            _logger.Info("*** simulator " + this.GetPrimaryKeyLong() + " starting " + cur_lat + " " + cur_long + " " + device_id);

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
            // update state

            cur_lat += lat_speed * speed_factor;
            cur_long += long_speed * speed_factor;

            try
            {
                // compute the device message
                DeviceMessage msg = new DeviceMessage(cur_lat, cur_long, 0, device_id, DateTime.Now);
                string msg_json = Newtonsoft.Json.JsonConvert.SerializeObject(msg);

                // make http request 
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(msg_json);

                HttpWebRequest req = HttpWebRequest.CreateHttp(_url);
                req.Method = "POST";
                req.ContentType = "application/json";
                req.ContentLength = data.Length;

                Stream newStream = req.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                _logger.Info("*** {0}-{1} sending request {2} {3}", _manager.GetPrimaryKeyLong(), this.GetPrimaryKeyLong(), cur_lat, cur_long);

                // wait for response
                var resp = await req.GetResponseAsync();
                resp.Close();

                // log the response 
                ++c_total_requests;
            }
            catch (WebException e)
            {
                _logger.Error(0, "*** WebException: ", e);

                WebExceptionStatus status = e.Status;
                if (status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse resp = (HttpWebResponse)e.Response;
                    if (((HttpWebResponse)resp).StatusCode != HttpStatusCode.OK)
                        ++c_failed_requests;
                }
            }
            catch (Exception e)
            {
                _logger.Error(0, "*** Exception: ", e);
            }
        }

        /// <summary>
        /// Periodically report results to the manager grain.
        /// </summary>
        /// <param name="o"></param>
        public async Task ReportResults(object o)
        {
            await _manager.SendResults(c_total_requests, c_failed_requests);
        }
    }
}
