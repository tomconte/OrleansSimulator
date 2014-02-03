using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IManagerGrain : IGrain
    {
        Task StartSimulators(ISimulationObserver observer);
        Task ReportResults(int results);
    }
}
