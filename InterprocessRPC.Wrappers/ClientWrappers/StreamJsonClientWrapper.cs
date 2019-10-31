using InterprocessRPC.Common;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers.ClientWrappers
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

        public Task<DateTime> GetServerTime()
        {
            return client.Proxy.GetServerTime();
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