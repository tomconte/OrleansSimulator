using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;

namespace GrainInterfaces
{
    public interface ISimulatorGrain : Orleans.IGrain
    {
        Task<string> StartSimulation(string name, IManagerGrain managerGrain);
        Task StopSimulation();
    }
}
