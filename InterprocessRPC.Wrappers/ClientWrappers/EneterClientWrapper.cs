using InterprocessRPC.Common;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers.ClientWrappers
{
    public class EneterClientWrapper : IClientWrapper
    {
        private readonly Eneter.Client<IEneterProxy> client = new Eneter.Client<IEneterProxy>();
        private IEneterProxy Proxy => client.RpcClient.Proxy;

        public Task<bool> CheckConnection()
        {
            return Task.FromResult(Proxy.CheckConnection());
        }

        public Task<string> GetHelloMessage(string name)
        {
            return Task.FromResult(Proxy.GetHelloMessage(name));
        }

        public Task<DateTime> GetServerTime()
        {
            return Task.FromResult(Proxy.GetServerTime());
        }

        public Task Start()
        {
            client.Start("eneterRpc");
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            client.Stop();
            return Task.CompletedTask;
        }
    }
}