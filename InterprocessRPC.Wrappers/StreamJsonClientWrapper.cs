using InterprocessRPC.Common;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers
{
    public class StreamJsonClientWrapper : IClientWrapper
    {
        private readonly Client<IProxy> client = new Client<IProxy>();

        public Task<bool> CheckConnection()
        {
            return client.Proxy.CheckConnection();
        }

        public Task<string> GetHelloMessage(string name)
        {
            return client.Proxy.GetHelloMessage(name);
        }

        public Task<ServerInfo> GetServerInfo()
        {
            return client.Proxy.GetServerInfo();
        }

        public Task Start()
        {
            return client.Start(Proxy.ProxyPipeName);
        }

        public Task Stop()
        {
            client.Stop();
            return Task.CompletedTask;
        }
    }
}