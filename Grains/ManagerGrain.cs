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
        private List<ISimulatorGrain> _cars = new List<ISimulatorGrain>();
        private int _totalSent;
        private Stopwatch _sw;
        private long _lastreport;

        /// <summary>
        /// Instantiate simulator grains and start the simulation on each.
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public async Task StartSimulators(ISimulationObserver observer)
        {
            _observer = observer;
            
            for (int i = 0; i < 1000; i++)
            {
                ISimulatorGrain grainRef = SimulatorGrainFactory.GetGrain(i);
                _cars.Add(grainRef);
                await grainRef.StartSimulation("Sim" + i, this);
            }

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
            if (_sw.ElapsedMilliseconds - _lastreport > 10000)
            {
                _observer.ReportResults(_totalSent / (int)(_sw.ElapsedMilliseconds / 1000));
                _lastreport = _sw.ElapsedMilliseconds;
            }

            return TaskDone.Done;
        }
    }
}
