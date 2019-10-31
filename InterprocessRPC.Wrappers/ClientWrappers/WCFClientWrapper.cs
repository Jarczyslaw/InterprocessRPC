using InterprocessRPC.Common;
using System;
using System.Threading.Tasks;

namespace InterprocessRPC.Wrappers.ClientWrappers
{
    public class WCFClientWrapper : IClientWrapper
    {
        private readonly WCF.Client<IProxy> client = new WCF.Client<IProxy>();

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
            client.Start(new WCFAddressProvider());
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            client.Stop();
            return Task.CompletedTask;
        }
    }
}