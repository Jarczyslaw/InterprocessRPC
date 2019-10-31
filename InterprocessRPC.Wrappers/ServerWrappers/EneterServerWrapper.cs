using InterprocessRPC.Common;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers.ServerWrappers
{
    public class EneterServerWrapper : IServerWrapper
    {
        private readonly Eneter.Server<IEneterProxy> server;

        public Action ListeningStart { get; set; }
        public Action ListeningStop { get; set; }
        public Action ClientConnected { get; set; }
        public Action ClientDisconnected { get; set; }

        public EneterServerWrapper()
        {
            server = new Eneter.Server<IEneterProxy>(new EneterProxy());
            server.RpcService.ResponseReceiverConnected += (s, e) => ClientConnected?.Invoke();
            server.RpcService.ResponseReceiverDisconnected += (s, e) => ClientDisconnected?.Invoke();
        }

        public Task Start()
        {
            server.Start("eneterRpc");
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