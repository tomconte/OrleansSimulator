using GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimulatorUI
{
    public class SimulationObserver : ISimulationObserver
    {
        public int c_sent { get; set; }

        public void ReportResults(long millis, int sent, int errors, Dictionary<long, int> all_sent, Dictionary<long, int> all_errors)
        {
            var avg = sent / (millis / 1000);
            c_sent = sent;
        }
    }
}