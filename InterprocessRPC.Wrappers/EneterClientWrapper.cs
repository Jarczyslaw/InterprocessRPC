using InterprocessRPC.Common;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers
{
    public class EneterClientWrapper : IClientWrapper
    {
        private readonly Eneter.Client<IProxy> client = new Eneter.Client<IProxy>();
        private IProxy Proxy => client.RpcClient.Proxy;

        public Task<bool> CheckConnection()
        {
            return Proxy.CheckConnection();
        }

        public Task<string> GetHelloMessage(string name)
        {
            return Proxy.GetHelloMessage(name);
        }

        public Task<DateTime> GetServerTime()
        {
            return Proxy.GetServerTime();
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