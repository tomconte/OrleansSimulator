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

using GrainInterfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class AggregatorGrain : GrainBase, IAggregatorGrain
    {
        private ISimulationObserver _observer;
        OrleansLogger _logger;
        private int _totalSent, _totalErrors;
        private long _totalSize;
        private Stopwatch _sw;
        IOrleansTimer _stattimer;

        static int REPORT_PERIOD = 30; // seconds


        // note where observer notifications are to be delivered to 
        public Task SetObserver(ISimulationObserver observer)
        {
            _observer = observer;
            return TaskDone.Done;
        }


        // report results as notication 
        public Task ReportResults(object o)
        {
            _observer.ReportResults(_sw.ElapsedMilliseconds, _totalSent, _totalErrors, _totalSize);
            return TaskDone.Done;
        }



        public override Task ActivateAsync()
        {
            _logger = base.GetLogger("Aggregator");

            _sw = new Stopwatch();
            _sw.Start();

            _stattimer = RegisterTimer(ReportResults, null, 
                    TimeSpan.FromSeconds(REPORT_PERIOD), TimeSpan.FromSeconds(REPORT_PERIOD));

            return base.ActivateAsync();
        }



        public Task AggregateResults(List<HttpWebResponse> results)
        {
            // Simple aggregations examples
            _totalSent += results.Count;
            _totalErrors += results.Count(r => r.StatusCode != HttpStatusCode.OK);
            _totalSize += results.Aggregate(0L, (t, n) => t + n.ContentLength);

            return TaskDone.Done;
        }
    }
}
