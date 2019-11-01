using Grpc.Core;
using InterprocessRPC.gRPC;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers.ServerWrappers
{
    public class gRPCServerWrapper : IServerWrapper
    {
        private Server server;

        public Action ListeningStart { get; set; }
        public Action ListeningStop { get; set; }
        public Action ClientConnected { get; set; }
        public Action ClientDisconnected { get; set; }

        public Task Start()
        {
            server = new Server
            {
                Services = { ProxyService.BindService(new ProxyServiceImpl()) },
                Ports = { new ServerPort("localhost", 1234, ServerCredentials.Insecure) }
            };
            server.Start();
            ListeningStart?.Invoke();
            return Task.CompletedTask;
        }

        public async Task Stop()
        {
            await server?.ShutdownAsync();
            ListeningStop?.Invoke();
        }
    }
}