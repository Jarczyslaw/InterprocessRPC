using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers
{
    public interface IServerWrapper
    {
        Action ListeningStart { get; set; }
        Action ListeningStop { get; set; }
        Action ClientConnected { get; set; }
        Action ClientDisconnected { get; set; }

        Task Start();

        Task Stop();
    }
}