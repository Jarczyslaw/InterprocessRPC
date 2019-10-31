using InterprocessRPC.Common;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers.ServerWrappers
{
    public class StreamJsonServerWrapper : IServerWrapper
    {
        private readonly Server<IProxy> server = new Server<IProxy>();

        public Action ListeningStart { get; set; }

        public Action ListeningStop { get; set; }

        public Action ClientConnected { get; set; }

        public Action ClientDisconnected { get; set; }

        public StreamJsonServerWrapper()
        {
            server.ClientConnected += _ => ClientConnected?.Invoke();
            server.ClientDisconnected += _ => ClientDisconnected?.Invoke();
            server.ListeningStart += () => ListeningStart?.Invoke();
            server.ListeningStop += _ => ListeningStop?.Invoke();
        }

        public Task Start()
        {
            return server.Start(Proxy.ProxyPipeName, () => new Proxy());
        }

        public Task Stop()
        {
            return server.Stop();
        }
    }
}