using InterprocessRPC.Common;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers
{
    public class WCFServerWrapper : IServerWrapper
    {
        private readonly WCF.Server<IProxy> server = new WCF.Server<IProxy>();

        public Action ListeningStart { get; set; }
        public Action ListeningStop { get; set; }
        public Action ClientConnected { get; set; }
        public Action ClientDisconnected { get; set; }

        public Task Start()
        {
            server.Start<Proxy>(new WCFAddressProvider());
            ListeningStart?.Invoke();
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            server.Stop();
            ListeningStop?.Invoke();
            return Task.CompletedTask;
        }
    }
}