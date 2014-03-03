using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrainInterfaces;
using System.Diagnostics;

namespace Grains
{
    public class ManagerGrain : GrainBase, IManagerGrain
    {
        private ISimulationObserver _observer;
        private List<ISimulatorGrain> _sims = new List<ISimulatorGrain>();
        private int _totalSent;
        private Stopwatch _sw;
        private long _lastreport;

        static int REPORT_PERIOD = 10; // seconds

        /// <summary>
        /// Instantiate simulator grains and start the simulation on each.
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public async Task StartSimulators(int start, int count, ISimulationObserver observer)
        {
            _observer = observer;

            List<Task> tasks = new List<Task>();

            for (int i = start; i < start+count; i++)
            {
                ISimulatorGrain grainRef = SimulatorGrainFactory.GetGrain(i);
                _sims.Add(grainRef);
                tasks.Add(grainRef.StartSimulation(i, this));
            }

            await Task.WhenAll(tasks);

            Console.WriteLine(count + " simulators started.");

            _sw = new Stopwatch();
            _sw.Start();
            _lastreport = _sw.ElapsedMilliseconds;
        }

        /// <summary>
        /// The simulator grains will call this method to report results.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public Task ReportResults(int results)
        {
            _totalSent += results;
            
            // Call the observer from time to time to report aggregated results
            if (_sw.ElapsedMilliseconds - _lastreport > REPORT_PERIOD * 1000)
            {
                _observer.ReportResults(_totalSent / (int)(_sw.ElapsedMilliseconds / 1000));
                _lastreport = _sw.ElapsedMilliseconds;
            }

            return TaskDone.Done;
        }
    }
}
